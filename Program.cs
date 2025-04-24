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
                        Console.WriteLine("Error! Input contains duplicate values.");
                        if (!PromptRetry()) return null;
                        continue;
                    }

                    // check range: values must be from 0 to nums.length
                    if(nums.Any(x=> x<0 || x>nums.Length)){
                        Console.WriteLine($"Error! Numbers must be between 0 and {nums.Length}");
                        if (!PromptRetry()) return null;
                        continue;
                    }

                    return nums;
                } catch {
                    Console.WriteLine("Error! Invalid input format. Write in the example format: 3,0,1");
                    if (!PromptRetry()) return null;
                    continue;
                }
            }
        }
        private bool PromptRetry(){
            Console.Write("Try again? (y/n): ");
            string response = Console.ReadLine().Trim().ToLower();
            return response =="y";
        }

    }
    public class OutputHandler{
        public void DisplayResult (int missingNumber){
            Console.WriteLine($"The missing number is: {missingNumber}");
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
            var numbers = _inputHandler.GetNumbersFromUser();
            if (numbers == null) {
                return; // user chose not to retry
            }
            var missing = _finder.FindMissingNumber(numbers);
            _outputHandler.DisplayResult(missing);
        }
    }
    public static class Program{
        static void Main(string[] args){
            IMissingNumberFinder finder = new MissingNumberFinder();
            InputHandler inputHandler= new InputHandler();
            OutputHandler outputHandler= new OutputHandler();
            while (true) {
                Game game = new Game(finder, inputHandler, outputHandler);
                game.Run();

                Console.Write("Try again? (y/n): ");
                string response = Console.ReadLine().Trim().ToLower();
                if (response == "n"){
                    Console.WriteLine("Thank you for using the program! Goodbye!");
                    break;
                }
                if (response != "n" && response != "y"){
                    Console.WriteLine("Invalid input. Write y or n.");
                    continue;
                }
            }
        }
    }
}