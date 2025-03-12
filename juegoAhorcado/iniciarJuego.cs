using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace juegoAhorcado
{
    public class iniciarJuego
    {
        private bool gameOver;
        private int healthPoints;
        private StringBuilder hearts = new StringBuilder();
        private List<string> words = new List<string> { "ornitorrinco", "oftalmologo", "hamburguesa", "basquetbol", "fingerita" };
        

        public iniciarJuego()
        {
            setHealthPoints();
        }

        public string selectRandomWord()
        {
            Random random = new Random();
            //los numeros random que .Next() puede generar son desde 0 - word.Count - 1
            //Osea: 0, 1, 2, ,3 ,4. Por lo que no es necesario restar -1 a word.Count
            int selectedWord = random.Next(words.Count);
            return words[selectedWord];
        }
        public void setHealthPoints()
        {
            Console.Write("Cantidad de Puntos de vida: ");
            string intput = Console.ReadLine();
            if (int.TryParse(intput, out int healthPoints))
            {
                this.healthPoints = healthPoints;
                for(int i = 0; i < healthPoints; ++i)
                {
                    hearts.Append("*");
                }
            }
            else
            {
                Console.WriteLine("Entrada invalida");
                setHealthPoints();
            }
        }
        public void removeHealthPoint()
        {
            hearts.Remove(hearts.Length - 1, 1);
        }
        public void jugar()
        {
            gameOver = false;
            bool letterFound = false;
            bool wordFound = false;

            //word = palabra random
            string word = selectRandomWord();
            //actualizar displayword
            char[] displayWord = new char[word.Length];
            for (int i = 0; i < word.Length; ++i)
            {
                displayWord[i] = '_';
            }
            //cuantos intentos tendra el jugador
            //setHealthPoints();
            Console.WriteLine($"Puntos de vida iniciales: {hearts}");
            Console.WriteLine();
            while (gameOver == false)
            {
                letterFound = false;

                Console.Write("Letra: ");
                char letter = Console.ReadKey().KeyChar;
                Console.WriteLine();

                //actualizar el displayword si letter existe en word
                for (int i = 0; i < word.Length; ++i)
                {
                    if(letter == word[i])
                    {
                        displayWord[i] = letter;
                        letterFound = true;
                    }
                }
                wordFound = true;
                //revisar si displayWord contiene "_" para saber si se encontro la palabra
                for(int i = 0;i < displayWord.Length; ++i)
                {
                    if (displayWord[i] == '_')
                    {
                        wordFound = false;
                        break;
                    }

                }

                if (wordFound)
                {
                    Console.WriteLine("You Win!");
                    Console.WriteLine(new string(displayWord));
                    return;
                }
                if (letterFound == false)
                {
                    --healthPoints;
                    removeHealthPoint();
                }
                if (healthPoints < 1)
                {
                    gameOver = true;
                    Console.WriteLine("Game Over");
                    return;
                }
                Console.Write("Palabra: ");
                Console.WriteLine(new string(displayWord));
                Console.WriteLine($"Puntos de vida: {hearts}");
                Console.WriteLine();

            }
        }
    }
}
