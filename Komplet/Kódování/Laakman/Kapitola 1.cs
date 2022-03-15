using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;

namespace C_Sharp
{
	static class Laakman_Kap1
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///1.1 Implement an algorithm to determine if a string has all unique characters. What if you  can not use additional data structures?
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static bool cviceni_1(List<char> retezec)
		{
			///1.1 Napište algoritmus, který zjistí, jestli jsou všechny znaky v řetězci obsaženy jen jednou. 
			bool[] znaky_nalezeny = new bool[256];
			int delka_retezce = retezec.Count;
			for (int i = 0; i < delka_retezce; i++)
			{
				if (znaky_nalezeny[retezec[i]])
					return false;
				else znaky_nalezeny[retezec[i]] = true;
			}
			return true;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//1.2 Napište kód, který převrátí C-řetězec (V C-řetěci je řetězec “abcd” reprezentován jako 5 znaků, všetně null znaku nas konci.)
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static List<char> cviceni_2(List<char> retezec)
		{
			char pomocna_promenna;
			int max_index = retezec.Count - 1;
			for (int i = 0; i <= max_index / 2; i++)
			{
				pomocna_promenna = retezec[i];
				retezec[i] = retezec[max_index - i];
				retezec[max_index - i] = pomocna_promenna;
			}
			retezec[0] = '\0';
			return retezec;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///1.3 Navrhněte algoritmus, který odstraní duplicitní znaky v řetězci bet použití následujícího bufferu. (Pozn. jedna nebo dvě proměnné navíc jsou v pořádku, kopie pole nikoliv.)
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static List<char> cviceni_3(List<char> retezec)
		{
			for (int aktualni_index = 0; aktualni_index < retezec.Count; aktualni_index++)
			{
				for (int index_porovnani = aktualni_index + 1; index_porovnani < retezec.Count; index_porovnani++)
				{
					if (retezec[aktualni_index] == retezec[index_porovnani])
					{
						retezec.RemoveAt(index_porovnani);
						index_porovnani--;
					}
				}
			}
			return retezec;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///1.4 Napište algoritmu, který zjistí, jestli dva řetězce jsou anagramy, či nikoliov.
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static bool jsou_anagramy(List<char> retezec1, List<char> retezec2)
		{
			if (retezec1.Count != retezec2.Count)
				return false;
			bool nalezeno = false;
			for (int i = 0; i < retezec1.Count; i++)
			{
				int j = i;
				while (j < retezec1.Count && !nalezeno)
				{
					if (retezec1[i] == retezec2[j])
					{
						nalezeno = true;
						char pomocna = retezec2[i];
						retezec2[i] = retezec2[j];
						retezec2[j] = pomocna;
					}
					else { j++; }
				}
				if (j >= retezec1.Count) return false;
			}
			return true;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//1.5 Napište metodu, která nahradí všechny mezery v řetězci ‘%20’.
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static List<char> cviceni_5(List<char> retezec)
		{
			int puvodni_delka_retezce = retezec.Count; int pocet_mezer = 0;
			//spočtení mezer a pridani nastaveni na konci retezce *
			for (int i = 0; i < puvodni_delka_retezce; i++)
			{
				if (retezec[i] == ' ')
				{
					pocet_mezer++;
					retezec.Add('*'); retezec.Add('*');
				}
			}
			//Presunuti - na konci minuleho kroku "A B C" -> "A BC****". pak postupně z pravého kraje přesouvám znaky na konec hvězdiček.
			//po druhém průchodu cyklu "A B***C" -> držíme si 
			int posledni_nepresunuty_znak = puvodni_delka_retezce - 1;
			int krajni_prava_hvezda = retezec.Count - 1;
			while (pocet_mezer > 0)
			{
				if (retezec[posledni_nepresunuty_znak] != ' ')
				{
					retezec[krajni_prava_hvezda] = retezec[posledni_nepresunuty_znak];
					posledni_nepresunuty_znak--;
					krajni_prava_hvezda--;
				}
				else
				{
					retezec[krajni_prava_hvezda] = '%';
					retezec[krajni_prava_hvezda - 1] = '0';
					retezec[krajni_prava_hvezda - 2] = '2';
					posledni_nepresunuty_znak--;
					krajni_prava_hvezda -= 3;
					pocet_mezer--;
				}
			}
			return retezec;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//1.6 Napište program, který zarotuje matici o 90 stupňů.
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static int[,] cviceni_6(int[,] matice)
		{
			int sirka;
			int a, b; int max_index;
			sirka = matice.GetLength(1);
			max_index = sirka - 1;
			for (int i = 0; i < sirka; i++)
			{
				for (int j = i; j < sirka - i - 1; j++)
				{
					a = matice[j, max_index - i];
					matice[j, max_index - i] = matice[i, j];

					b = matice[max_index - i, max_index - j];
					matice[max_index - i, max_index - j] = a;
					a = b;

					b = matice[max_index - j, i];
					matice[max_index - j, i] = a;
					a = b;
					matice[i, j] = a;
				}
			}
			return matice;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//1.7 Napište algoritmus, jestliže jeden element MxN matice je 0, tak vynulujete celý řádek a celý sloupec.
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static int[,] cviceni_7(int[,] matice)
		{
			int pocet_sloupcu = matice.GetLength(1);
			int pocet_radku = matice.GetLength(0);
			bool[] ve_sloupci_nalezena_nula = new bool[pocet_sloupcu];
			for (int radek = 0; radek < pocet_radku; radek++)
			{
				int sloupec = 0; bool nevynulovano = true;
				while (sloupec < pocet_sloupcu && nevynulovano)
				{
					if (matice[radek, sloupec] == 0)
					{
						ve_sloupci_nalezena_nula[sloupec] = true;
						vynuluj_predchozi_ve_sloupci(matice, sloupec, radek);
						vynuluj_radek(matice, radek, sloupec);
						nevynulovano = false;
					}
					else
					{
						if (ve_sloupci_nalezena_nula[sloupec])
						{
							matice[radek, sloupec] = 0;
						}
					}
					sloupec++;
				}
			}
			return matice;
		}
		private static int[,] vynuluj_predchozi_ve_sloupci(int[,] matice, int mazany_sloupec, int aktualni_radek)
		{
			for (int radek = aktualni_radek; radek >= 0; radek--)
			{
				matice[radek, mazany_sloupec] = 0;
			}
			return matice;
		}
		private static int[,] vynuluj_radek(int[,] matice, int radek, int aktualni_sloupec)
		{
			for (int sloupec = 0; sloupec < aktualni_sloupec; sloupec++)
			{
				matice[radek, sloupec] = 0;
			}
			for (int sloupec = aktualni_sloupec + 1; sloupec < matice.GetLength(1); sloupec++)
			{
				matice[radek, sloupec] = 0;
			}
			return matice;
		}
	}
}

