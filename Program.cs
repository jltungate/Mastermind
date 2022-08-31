//Mastermind Console Game
//Jonathan Tungate - 8/30/2022
//Quadax Inc Programming Exercise

using System;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuePlaying = true;
            while (continuePlaying){
                continuePlaying = PlayGame();
            }
            Console.WriteLine("Thank you for playing.");
        }

        //Creates key and begins game
        //Loops through guesses 10 times or until correct key is guessed
        //Asks if user wants to play again
        public static bool PlayGame(){
            List<string> key = GenerateKey();

            Console.WriteLine("Mastermind has begun, please guess a 4 digit number.");
            
            bool guessIsCorrect = false;
            int guessCounter = 10;
            while (!guessIsCorrect && guessCounter > 0){
                var guess = Console.ReadLine();
                if (ValidateGuess(guess)){
                    if (IsGuessCorrect(guess, key)){
                        guessIsCorrect = true;
                    }
                    else{
                        guessCounter--;
                        var hint = GetHint(guess, key);
                        Console.WriteLine(hint + " | " + guessCounter + " attempts remaining. Please Guess Again.");
                    }
                }
                else{
                    Console.WriteLine("Guess may only consist of a 4 digit number.");
                }
            }
            if (guessIsCorrect){
                Console.WriteLine("Congratulations you've guessed correct!)");
            }
            else{
                Console.WriteLine("Better luck next time.");
            }
            Console.WriteLine("Would you like to play again? (Enter \"Y\" for Yes, or \"N\" for No)");
            bool validateResponse = false;
            
            string response = "";
            while (!validateResponse) {
                response = Console.ReadLine();     
                if (response == "Y" || response == "N"){
                    validateResponse = true;
                }
                else{
                    Console.WriteLine("Please only enter \"Y\" for Yes or \"N\" for No");
                }
            }

            if (response == "Y"){
                return true;
            }
            else {
                return false;
            }

        }

        //return a bool if the user input guess matches key or not
        public static bool IsGuessCorrect(string guess, List<string> key){
            string keystring = "";
            foreach (string k in key){
                keystring += k;
            }
            if (guess == keystring){
                return true;
            }
            else{
                return false;
            }
        }

        //Mastermind rules
        //- sign for every digit correct but in wrong position
        //+ sign for every digit correct and in correct position
        //print + first - second nothing for incorrect digits as a string
        public static string GetHint(string guess, List<string> key){
            string hintString = ""; 
            for (int i = 0; i < guess.Length; i++){
                string curletter = guess[i].ToString();
                if (key.Contains(curletter)){
                    int index = key.IndexOf(curletter);
                    if (index == i){
                        hintString = "+" + hintString;
                    }
                    else{
                        hintString += "-";
                    }
                }      
            }
            return hintString;  
        }

        //Creates 4 random numbers and adds to list as string
        public static List<string> GenerateKey(){
            List<string> keyList = new List<string>();
            Random rand = new Random();
            for (int i = 0; i < 4; i++){
                string digit = rand.Next(1, 7).ToString();
                keyList.Add(digit);
            }
            return keyList;
        }

        //Prevents guess from being null, wrong length, or not a numeric
        public static bool ValidateGuess(string? guess){
            if (guess != null){
                if (guess.Length != 4)
                    return false;
                else if (guess.All(c => c >= '0' && c <= '9'))
                    return true;
                else
                    return false; 
                }
            else
                return false;
        }
    }
}
