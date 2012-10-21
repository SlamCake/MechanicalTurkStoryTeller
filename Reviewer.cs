#region Copyright & license notice
/*
 * Copyright: Copyright (c) 2007 Amazon Technologies, Inc.
 * License:   Apache License, Version 2.0
 */
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Amazon.WebServices.MechanicalTurk;
using Amazon.WebServices.MechanicalTurk.Domain;

namespace Reviewer
{
    /// <summary>
    /// The Reviewer sample application will retrieve the completed assignments for a given HIT,
    /// output the results and approve or reject the assignment.
    /// 
    /// NOTE: You will need to have the HIT ID of an existing HIT that has been accepted, completed and
    /// submitted by a worker.
    /// 
    /// The following concepts are covered:
    /// - Retrieve results for a HIT
    /// - Approve or reject assignments
    /// </summary>
    /// <remarks>
    /// NOTE: You will need to configure your AWS access key information in the application config (app.config)
    /// prior to running this sample
    /// </remarks>
    public class Reviewer
    {
        private IList<Assignment> assignments;
        private String[] answers;
        private Reviewer myReviewer;

        public Reviewer MyReviewer
        {
            get { return myReviewer; }
            set { myReviewer = value; }
        }

        public String[] Answers
        {
            get { String[] returnable = answers; answers.Initialize(); return returnable; }
            set { answers = value; }
        }
        private Object[] questions;

        public Object[] Questions
        {
            get { Object[] returnable = questions; questions.Initialize(); return returnable; }
            set { questions = value; }
        }



        public IList<Assignment> Assignments
        {
            get { return assignments; }
            set { assignments = value; }
        }
        private SimpleClient client = new SimpleClient();

        /// <summary>
        /// Prints the submitted results of a HIT when provided with a HIT ID and allows for
        /// acceptance or rejection of work
        /// </summary>
        /// <remarks>
        /// If you don't know a valid HIT ID, you can use a HIT created by another example, such
        /// as HelloWorld.
        /// 
        /// NOTE: The CreateHitUI example copies the ID of the created HIT in the clipboard, which
        /// makes it easy to use in conjunction with the Reviewer example
        /// </remarks>
        /// <param name="hitID">ID of hit to review</param>
        public void ReviewAnswers(string hitID)
        {
            SimpleClient client = new SimpleClient();
            HIT myHIT = client.GetHIT(hitID);
            int count = 0;
            assignments = GetAssignments(hitID);
            if (assignments == null || assignments.Count == 0)
            {
                Console.WriteLine("No assignments found for HIT {0}", hitID);
            }
            else
            {
                foreach (Assignment a in assignments)
                {
                    // check submitted assignments for answer data
                    Console.WriteLine(count + " : " + "Assignment {0} for worker {2} ({1})", a.AssignmentId, a.AssignmentStatus, a.WorkerId);
                    if (a.AssignmentStatus == AssignmentStatus.Submitted)
                    {
                        QuestionForm myForm = QuestionUtil.DeserializeQuestionForm(myHIT.Question);
                        foreach (QuestionFormQuestion q in myForm.Question)
                        {
                            PrintQuestion(q);
                        }
                        QuestionFormAnswers answers = QuestionUtil.DeserializeQuestionFormAnswers(a.Answer);
                        foreach (QuestionFormAnswersAnswer answer in answers.Answer)
                        {
                            PrintAnswer(answer);
                        }
                    }
                    count++;
                }
            }
        }

        #region Helpers
        /// <summary>
        /// Gets assignments for a HIT ID. If no assignments can be found,
        /// it asks for a different HIT ID
        /// </summary>
        public IList<Assignment> GetAssignments(string hitID)
        {
            IList<Assignment> ret = null;

            while (ret == null || ret.Count == 0)
            {
                if (string.IsNullOrEmpty(hitID))
                {
                    Console.Write("Please enter the ID of the HIT to review:");
                    hitID = Console.ReadLine().Trim();
                }

                try
                {
                    ret = client.GetAllAssignmentsForHIT(hitID);
                    if (ret.Count == 0)
                    {
                        Console.WriteLine("No assignments for this hit");
                        hitID = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    hitID = null;
                }
            }

            return ret;
        }

        /// <summary>
        /// Print basic information about the answer
        /// </summary>
        private void PrintAnswer(QuestionFormAnswersAnswer answer)
        {
            this.Answers = answer.Items;
            Console.WriteLine("   Answer for question {0}", answer.QuestionIdentifier);
            for (int i = 0; i < answer.Items.Length; i++)
            {
                Console.WriteLine("    {0} : {1}", answer.ItemsElementName[i], answer.Items[i]);
            }
        }
        private void PrintQuestion(QuestionFormQuestion q)
        {
            this.Questions = q.QuestionContent.Items;
            for (int i = 0; i < q.QuestionContent.Items.Length; i++)
            {
                Console.WriteLine(q.QuestionContent.Items[i].ToString());
            }
        }

        #endregion

    }
}
    
                            