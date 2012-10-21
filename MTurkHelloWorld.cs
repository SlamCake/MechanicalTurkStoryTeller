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
    /// <summary>
    /// The MTurk Hello World sample application creates a simple HIT via the Mechanical Turk .Net SDK
    /// </summary>
    /// <remarks>
    /// NOTE: You will need to configure your AWS access key information in the application config (app.config)
    /// prior to running this sample
    /// </remarks>
    public class MTurkHelloWorld
    {
        private SimpleClient client = new SimpleClient();

        /// <summary>
        /// Check if there are enough funds in your account in order to create the HIT
        /// on Mechanical Turk
        /// </summary>
        /// <returns>true if there are sufficient funds. False if not.</returns>
        public bool HasEnoughFunds()
        {
            return (client.GetAvailableAccountBalance() > 0);
        }

        /// <summary>
        /// Creates the simple HIT
        /// </summary>
        public void CreateHIT(String story)
        {
            HIT h = client.CreateHIT("Answer a question",
                "This is a HIT created by the Mechanical Turk SDK.  Continue the story below.",
                new decimal(0.00),
                story,
                1);

            // output ID and Url of new HIT (URL where HIT is available on the Mechanical Turk worker website)
            Console.WriteLine("Created HIT: {0} ({1})", h.HITId, client.GetPreviewURL(h.HITTypeId));
        }
    }
}
