using System;
using System.Linq;

namespace missingNumberGame{
    public interface IMissingNumberFinder{
        int FindMissingNumber (int[] nums);
    }
    public class MissingNumberFinder: IMissingNumberFinder{
        public int FindMissingNumber (int[] nums){
            int n = nums.Length;    
            int expectedSum = n*(n+1)/2;
            int actualSum = nums.Sum();
            return expectedSum - actualSum;
        }
    }

    public class InputHandler{
        public int[] GetNumbersFromUser(){
            while(true){
                Console.Write("Enter numbers sepearted by commas (example 3,0,1): ");
                string input = Console.ReadLine();
                try{
                    var nums = input.Split(',')
                                .Select(str =>{
                                    if (!int.TryParse(str.Trim(), out int result))
                                        throw new FormatException();
                                    return result;
                                }).ToArray();

                    // check duplicates
                    if (nums.Length != nums.Distinct().Count()){
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.WriteLine("Error! Input contains duplicate values.");
                        Console.ResetColor();
                        if (!PromptRetry()) return null;
                        continue;
                    }

                    // check range: values must be from 0 to nums.length
                    if(nums.Any(x=> x<0 || x>nums.Length)){
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error! Numbers must be between 0 and {nums.Length}");
                        Console.ResetColor();
                        if (!PromptRetry()) return null;
                        continue;
                    }

                    return nums;
                } catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Invalid input format. Write in the example format: 3,0,1");
                    Console.ResetColor();
                    if (!PromptRetry()) return null;
                    continue;
                }
            }
        }
        private bool PromptRetry(){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Try again? (y/n): ");
            Console.ResetColor();
            string response = Console.ReadLine().Trim().ToLower();
            return response =="y";
        }

    }
    public class OutputHandler{
        public void DisplayResult (int missingNumber){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🎯 The missing number is: {missingNumber}");
            Console.ResetColor();
        }
    }

    public class Game{
        private readonly IMissingNumberFinder _finder;
        private readonly InputHandler _inputHandler;
        private readonly OutputHandler _outputHandler;

        public Game(IMissingNumberFinder finder, InputHandler inputHandler, OutputHandler outputHandler){
            _finder = finder;
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }
        public void Run(){
            Console.Clear();
            
            Console.WriteLine("\n=================================================");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" ˚.🎀༘⋆ Welcome to the Missing Number Game! ⋆🎀༘.˚");
            Console.ResetColor();
            Console.WriteLine("=================================================\n");
            
            Console.WriteLine("Instructions:");
            Console.WriteLine("🌱 Enter a sequence of numbers separated by commas.");
            Console.WriteLine("🌱 The numbers should be unique and within the range 0 to n.");
            Console.WriteLine("🌱 One number will be missing, and I'll find it!\n");

            var numbers = _inputHandler.GetNumbersFromUser();
            if (numbers == null) {
                return; // user chose not to retry
            }
            var missing = _finder.FindMissingNumber(numbers);
            _outputHandler.DisplayResult(missing);
        }
    }

    public static class Program{
        public static void ExitWithProgress(){
            Console.WriteLine("\nThank you for using the program. Exiting...");
            int totalBlocks = 20;
            int sleepTime = 100;

            for (int i=0; i<totalBlocks; i++){
                int percent = i*100/totalBlocks;
                Console.ForegroundColor = ConsoleColor.Green;

                string progressBar = "[" + new string('#',i)
                    + new string(' ', totalBlocks-i) + $"] {percent}% ";
                Console.Write($"\r{progressBar}");
                Console.ResetColor();
                Thread.Sleep(sleepTime);
            }
            Console.WriteLine("\nGoodBye! See you again!👋");
        }
        static void Main(string[] args){
            IMissingNumberFinder finder = new MissingNumberFinder();
            InputHandler inputHandler= new InputHandler();
            OutputHandler outputHandler= new OutputHandler();
            while (true) {
                Game game = new Game(finder, inputHandler, outputHandler);
                game.Run();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Try again? (y/n): ");
                Console.ResetColor();
                string response = Console.ReadLine().Trim().ToLower();
                if (response == "n"){
                    ExitWithProgress();
                    break;
                }
                if (response != "n" && response != "y"){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Write y or n.");
                    Console.ResetColor();
                    continue;
                }
            }
        }
    }
}