#region Copyright & license notice
/*
 * Copyright: Copyright (c) 2007 Amazon Technologies, Inc.
 * License:   Apache License, Version 2.0
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Amazon.WebServices.MechanicalTurk;
using Amazon.WebServices.MechanicalTurk.Domain;

namespace Reviewer
{
    public class Program
    {
        List<Assignment> myAssignments;
        Reviewer rev = new Reviewer();
        HitReport hr = new HitReport();
        static void Main()
        {
            int n = 99;
            string s;
            string newStory = "";
            Program myProgram = new Program();
            while (n != 0)
            {
                Console.WriteLine("0 - Quit");
                Console.WriteLine("1 - PrintHits");
                Console.WriteLine("2 - ReviewAnswer");
                Console.WriteLine("3 - ContinueHIT");
                Console.WriteLine("4 - CreateNewHIT");
                Console.Write("Please enter your selection: ");
                s = Console.ReadLine();
                n = int.Parse(s);
                switch (n)
                {
                    case 1:
                        myProgram.PrintHits();
                        break;
                    case 2:
                        Console.Write("Please enter HIT ID to check: ");
                        s = Console.ReadLine();
                        myProgram.setAssignments(myProgram.ReviewAnswer(s));
                        break;
                    case 3:
                        if (myProgram.getAssignments().Equals(null))
                        {
                            Console.Write("Please run '2' first");
                            break;
                        }
                        if (myProgram.getAssignments().Count == 0)
                        {
                            Console.Write("No HITS available to contiue");
                            break;
                        }
                        Console.Write("Which # entry in ReviewAnswer()?");
                        s = Console.ReadLine();
                        int x = int.Parse(s);
                            //QuestionFormAnswers answers = QuestionUtil.DeserializeQuestionFormAnswers(a.Answer);
                            //foreach (QuestionFormAnswersAnswer answer in answers.Answer)
                            //{
                            //    PrintAnswer(answer);                            
                            //}  
                       // QuestionFormAnswers answers = QuestionUtil.DeserializeQuestionFormAnswers(myProgram.getAssignments().ToArray()[x].Answer);
                        Console.WriteLine("Creating a new HIT with: "+myProgram.rev.Answers[x]);
                        for (int i = 0; i < myProgram.rev.Questions.Length; i++)
                        {
                            newStory += myProgram.rev.Questions[i].ToString();
                        }
                        newStory += " "+myProgram.rev.Answers[x];
                        myProgram.ContinueHIT(newStory);
                        break;
                    case 4:
                        Console.Write("How would you like to begin the story?");
                        s = Console.ReadLine();
                            //QuestionFormAnswers answers = QuestionUtil.DeserializeQuestionFormAnswers(a.Answer);
                            //foreach (QuestionFormAnswersAnswer answer in answers.Answer)
                            //{
                            //    PrintAnswer(answer);                            
                            //}  
                        myProgram.CreateNewHIT(s);
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }

        }
        public void PrintHits()
        {
            HitReport report = new HitReport();
            report.PrintAllHits();
            Console.Write("Press any key to continue ...");
            Console.ReadLine();
        }
        public IList<Assignment> ReviewAnswer(String hitID)
        {
            //string hitID = null;
            //if (args.Length > 0)
            //{
            //    hitID = args[0];
            //}
            rev = new Reviewer();
            rev.ReviewAnswers(hitID);
            Console.Write("Press any key to continue ...");
            Console.ReadLine();
            return rev.GetAssignments(hitID);
        }
        public void CreateNewHIT(String story)
        {
            MTurkHelloWorld world = new MTurkHelloWorld();
            if (world.HasEnoughFunds())
            {
                world.CreateHIT(story);
            }
            else
            {
                Console.WriteLine("You do not have enough funds to run this sample");
            }
            Console.Write("Press any key to continue ...");
            Console.ReadLine();
        }
        public void ContinueHIT(String story)
        {
            MTurkHelloWorld world = new MTurkHelloWorld();
            if (world.HasEnoughFunds())
            {
                world.CreateHIT(story);
            }
            else
            {
                Console.WriteLine("You do not have enough funds to run this sample");
            }
            Console.Write("Press any key to continue ...");
            Console.ReadLine();
        }
        public List<Assignment> getAssignments() { return myAssignments; }
        public void setAssignments(IList<Assignment> a) { myAssignments = (List<Assignment>)a; }
    }
}
