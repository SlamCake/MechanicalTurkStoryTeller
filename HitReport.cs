#region Copyright & license notice
/*
 * Copyright: Copyright (c) 2007 Amazon Technologies, Inc.
 * License:   Apache License, Version 2.0
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Amazon.WebServices.MechanicalTurk;
using Amazon.WebServices.MechanicalTurk.Domain;

namespace Reviewer
{
    public class HitReport
    {

        private QuestionFormQuestion myQuestion;

        public QuestionFormQuestion MyQuestion
        {
            get { return myQuestion; }
            set { myQuestion = value; }
        }
        private HitReport myHitReport;

        public HitReport MyHitReport
        {
            get { return myHitReport; }
            set { myHitReport = value; }
        }

        /// <summary>
        /// The HitReport sample application will output information about all active HITs
        /// of the configured requester.
        /// 
        /// The following concepts are covered:
        /// - Enumerate all HITs using the GetAllHITsEnumerator method
        /// - Output HIT information
        /// </summary>
        /// <remarks>
        /// NOTE: You will need to configure your AWS access key information in the application config (app.config)
        /// prior to running this sample
        /// </remarks>
        public void PrintAllHits()
        {
            // enumerate through all HITs (rather than loading them 
            // all in memory using GetAllHITs())
            SimpleClient client = new SimpleClient();
            // print a header
            Console.WriteLine("HIT ID                Expiration date   Status          Review status   Title");
            Console.WriteLine("=============================================================================");
            int count = 0;
            foreach (HIT h in client.GetAllHITsIterator())
            {
                Console.WriteLine(count+"{0,10}  {1:yyyy-mm-dd:hh:MM}  {2,-15} {3,-15} {4}",
                    h.HITId,
                    h.Expiration,
                    h.HITStatus,
                    h.HITReviewStatus,
                    h.Title);
                Console.WriteLine(h.Description);
                QuestionForm myForm = QuestionUtil.DeserializeQuestionForm(h.Question);
                foreach (QuestionFormQuestion q in myForm.Question)
                {
                    for (int i = 0; i < q.QuestionContent.Items.Length; i++)
                    {
                        Console.WriteLine(q.QuestionContent.Items[i].ToString());
                    }
                }

            }
            //if (h.HITStatus.ToString().Equals("Reviewable"))
            //{
            //    GetAssignmentsForHIT assigment = new GetAssignmentsForHIT();
            //    Console.WriteLine("   ---   TURTLE   ---   " + assigment.Request.ToString());
            //}
            //foreach (HIT h in client.GetAllReviewableHITs(typeID)) 
            //{ 
            //    GetAssignmentsForHIT(h.HITId);
            //}
            //foreach ( HIT h in client.GetAllReviewableHITsIterator())
            //{
            //    Console.WriteLine("{0,10}  {1:yyyy-mm-dd:hh:MM}  {2,-15} {3,-15} {4}",
            //        h.HITId,
            //        h.Expiration,
            //        h.HITStatus,
            //        h.HITReviewStatus,
            //        h.Title);
            //    Console.Write(h.Description.ToString());
            //    Console.Write(h.Question.ToString());
            //    Console.Write(h.);
            //    Console.Write(h.Request.ToString());
            //    Console.Write(h.Reward.ToString());
            //    Console.Write(h.RequesterAnnotation.ToString());

            //        //,
            //        //h.Question,
            //        //h.Request,
            //        //h.Reward,
            //        //h.RequesterAnnotation)
            //}
        }
    }
}
