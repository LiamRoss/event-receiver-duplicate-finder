using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Receiver_Duplicate_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            eventReceiverDuplicateFinder();
        }

        static void eventReceiverDuplicateFinder()
        {
            List<EventReceiverObject> eventReceivers = new List<EventReceiverObject>();
            string filePath;
            //string eventReceiverAll = "hi";

            // User inputs file path to Event Receiver text file
            Console.Clear();
            Console.WriteLine("In order to run this program, you must have generated a text file that " +
                "contains a properly-formatted list of all Event Receivers...\n");

            // Waits for valid .txt file
            bool fileValid = false;
            while (fileValid == false)
            {
                try
                {
                    Console.WriteLine("\nPlease input file path (with \".txt\" file extension):\n");
                    filePath = Console.ReadLine();
                    // Get the contents of the Event Receiver file, converts to list of EventReceiverObject
                    eventReceivers = fetchEventReceiversFromFile(filePath);

                    fileValid = true;
                }
                catch // If file doesn't exist
                {
                    Console.WriteLine("\nCan't find file, try again...");
                    Console.ReadKey();
                }
            }

            // Prints a report to a file, returns information to console
            findDuplicates(eventReceivers);

            Console.WriteLine("\nReport has been generated, press any key to exit...\n");
            Console.ReadKey();
        }

        // Takes file path, returns contents in List of EventReceiverObject
        static List<EventReceiverObject> fetchEventReceiversFromFile(string filePath)
        {
            List<EventReceiverObject> eventReceivers = new List<EventReceiverObject>();

            if (filePath == null)
            {
                throw new System.ArgumentException("Can't find file");
            }
            eventReceivers = eventReceiverListMaker(filePath);

            return eventReceivers;
        }

        // Takes contents of Event Receiver file, returns list of EventReceiverObject
        static List<EventReceiverObject> eventReceiverListMaker(string filePath)
        {
            List<EventReceiverObject> eventReceivers = new List<EventReceiverObject>();
            EventReceiverObject eventReceiver = new EventReceiverObject();
            char delimiter = ':';
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(":"))
                {
                    string[] keyAndValue = line.Split(delimiter);
                    string key = keyAndValue.First().Trim();
                    string value = keyAndValue.Last().Trim();

                    eventReceiver.assignValueToKey(key, value);
                }
                else if (eventReceiver.getUpgradedPersistedProperties() != null)
                {
                    eventReceivers.Add(eventReceiver);
                    eventReceiver = new EventReceiverObject();
                }
                counter++;
            }

            file.Close();

            Console.WriteLine("\nEVENT RECEIVERS PARSED:\n");

            int numOfParsed = 1;

            foreach (EventReceiverObject ero in eventReceivers)
            {
                Console.WriteLine("\n" + numOfParsed + ". NAME: " +
                    ero.getName() + " - TYPE: " + ero.getType() + " - SEQUENCENUMBER: " + ero.getSequenceNumber() + "\n");
                numOfParsed++;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
            return eventReceivers;
        }

        //Takes list of EventReceiverObject, creates report file, prints results to Console
        static void findDuplicates(List<EventReceiverObject> eventReceivers)
        {
            int totalMultiples;
            string pathString;
            string fileName;

            // Generate a dateTime for the local system to put on the report
            string cultureName = "en-GB";
            var culture = new CultureInfo(cultureName);
            string dateTime = DateTime.Now.ToString(culture);
            Console.WriteLine("\nReport Time: " + dateTime + "\n");

            // Read file folder path, generate if needed, assign to pathString
            Console.WriteLine("\nEnter path to folder containing report (will create if it doesn't exist):\n");
            pathString = Console.ReadLine();
            System.IO.Directory.CreateDirectory(pathString);

            // Read and generate .txt file at specified path, append to pathString to create complete path to file
            Console.WriteLine("\nEnter unique name for Report (MUST be unique or else will just append to existing file, don't forget the file extension \".txt:\"\n");
            fileName = Console.ReadLine();
            pathString = System.IO.Path.Combine(pathString, fileName);
            Console.WriteLine(pathString);

            // Format heading for generated .txt report
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@pathString, true))
            {
                int totalItems = eventReceivers.Count();
                file.WriteLine("Multiple Event Receivers Report");
                file.WriteLine("");
                file.WriteLine("REPORT TIME: " + dateTime);
                file.WriteLine("NUMBER OF ITEMS: " + totalItems);
                file.WriteLine("");
                file.WriteLine("");
                file.WriteLine("FILES WITH MULTIPLE COPIES:");
                file.WriteLine("");
            }
            totalMultiples = printDuplicates(eventReceivers, pathString);

            // Prints total number of files that contained multiple copies at bottom of text document
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@pathString, true))
            {
                file.WriteLine("NUMBER OF FILES WITH MULTIPLE COPIES: " + totalMultiples);
                file.WriteLine("");
            }
        }

        /// <summary>
        /// Parse through eventReceivers... don't stop till they're all removed from the list.
        ///
        /// For each time through while loop:
        /// 1.  Records Sequence Number and ID Number of eventReceiver at index 0 and any eventReciever 
        ///          that matches Name and Type of index 0.
        /// 2.  Then it removes eventReceiver at index 0 and any eventReciever that matches Name and 
        ///          Type of index 0.
        /// 3.  Finally, if there is more than one eventReceiver with that Name and Type, prints the 
        ///          Name and Type to .txt file, as well as a numbered list of all Sequence Numbers 
        ///          and ID Numbers.
        /// </summary>
        /// <param name="eventReceivers">List of Event Receiver Objects</param>
        /// <param name="pathString">The path to the file in which to save the report</param>
        /// <returns>totalMultiples - Total number of unique copied files.</returns>
        static int printDuplicates(List<EventReceiverObject> eventReceivers, string pathString) {
            int totalMultiples = 0;

            while (eventReceivers.Any())
            {
                // Sets eventReceiver at index 0's Name and Type as the reference strings
                string nameRef = eventReceivers[0].getName();
                string typeRef = eventReceivers[0].getType();
                // List of indexes that need to be removed.
                List<int> indexes = new List<int>();
                // Lists of sequenceNumbers and idNumbers held by matching eventReceivers.
                List<string> sequenceNumbers = new List<string>();
                List<string> idNumbers = new List<string>();
                // Number of files that match index 0.
                int numMultiples = 0;
                

                // For all matching eventReceivers (including index 0) adds index, sequenceNumber and 
                // idNumber to the relevant list, increases number of multiples.
                for (int i = 0; i < eventReceivers.Count(); i++)
                {
                    if (nameRef == eventReceivers[i].getName() && typeRef == eventReceivers[i].getType())
                    {
                        indexes.Add(i);
                        sequenceNumbers.Add(eventReceivers[i].getSequenceNumber());
                        idNumbers.Add(eventReceivers[i].getId());
                        numMultiples++;
                    }
                }

                // Reverses the indexes list, so that files are removed from the end (highest index numbers
                // will now be first) so as to not mess with index numbers.
                indexes.Reverse();

                // Remove all matching eventReceivers from list
                foreach (int i in indexes)
                {
                    eventReceivers.RemoveAt(i);
                }

                // If there was a match for the eventReceiver at index 0, numMultiples will be >1, and:
                // 1.   An entry in the .txt file will be created for that Name and Type.
                // 2.   A list of all Sequence Numbers and ID Numbers of the matches will be written in 
                //          the .txt file
                if (numMultiples > 1)
                {
                    totalMultiples++;

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@pathString, true))
                    {
                        int numSN = 1;

                        file.WriteLine("NAME: " + nameRef + " - TYPE: " + typeRef + " - NUMBER OF MULTIPLES: " + numMultiples);
                        file.WriteLine("");
                        file.WriteLine("SEQUENCE NUMBER - ID:");

                        foreach (string sn in sequenceNumbers)
                        {
                            int idIndex = numSN - 1;
                            file.WriteLine(numSN + ". " + sn + " - " + idNumbers[idIndex]);
                            numSN++;
                        }
                        file.WriteLine("");
                    }
                }
            }
            return totalMultiples;
        }
    }
}
