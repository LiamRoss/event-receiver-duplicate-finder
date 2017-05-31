using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Receiver_Duplicate_Finder
{
    class EventReceiverObject
    {
        string Id = null;
        string Name = null;
        string SiteId = null;
        string WebId = null;
        string HostId = null;
        string HostType = null;
        string ParentHostId = null;
        string ParentHostType = null;
        string Synchronization = null;
        string Type = null;
        string SequenceNumber = null;
        string Assembly = null;
        string Class = null;
        string Data = null;
        string Filter = null;
        string Credential = null;
        string ContextItemId = null;
        string ContextItemUrl = null;
        string ContextType = null;
        string ContextEventType = null;
        string ContextId = null;
        string ContextObjectId = null;
        string ContextCollectionId = null;
        string Url = null;
        string UpgradedPersistedProperties = null;

        // CONSTRUCTOR: Takes no values, all objects remain null
        public EventReceiverObject() { }

        // Takes the string key of an EventReciever and the string value associated with the key, and assigns that value to the key
        internal void assignValueToKey(string key, string value)
        {
            switch (key)
            {
                case "Id":
                    Id = value;
                    break;
                case "Name":
                    Name = value;
                    break;
                case "SiteId":
                    SiteId = value;
                    break;
                case "WebId":
                    WebId = value;
                    break;
                case "HostId":
                    HostId = value;
                    break;
                case "HostType":
                    HostType = value;
                    break;
                case "ParentHostId":
                    ParentHostId = value;
                    break;
                case "ParentHostType":
                    ParentHostType = value;
                    break;
                case "Synchronization":
                    Synchronization = value;
                    break;
                case "Type":
                    Type = value;
                    break;
                case "SequenceNumber":
                    SequenceNumber = value;
                    break;
                case "Assembly":
                    Assembly = value;
                    break;
                case "Class":
                    Class = value;
                    break;
                case "Data":
                    Data = value;
                    break;
                case "Filter":
                    Filter = value;
                    break;
                case "Credential":
                    Credential = value;
                    break;
                case "ContextItemId":
                    ContextItemId = value;
                    break;
                case "ContextItemUrl":
                    ContextItemUrl = value;
                    break;
                case "ContextType":
                    ContextType = value;
                    break;
                case "ContextEventType":
                    ContextEventType = value;
                    break;
                case "ContextId":
                    ContextId = value;
                    break;
                case "ContextObjectId":
                    ContextObjectId = value;
                    break;
                case "ContextCollectionId":
                    ContextCollectionId = value;
                    break;
                case "Url":
                    Url = value;
                    break;
                case "UpgradedPersistedProperties":
                    UpgradedPersistedProperties = value;
                    break;
                default:
                    Console.WriteLine("Error in your value... incorrect key: " + key + 
                        " is not a valid key, check your text file");
                    break;
            }
        }

        // NOTE: have not completed all get functions, only ones I used and some other ones
        internal string getId()
        {
            return Id;
        }

        internal string getName()
        {
            return Name;
        }

        internal string getSiteId()
        {
            return SiteId;
        }

        internal string getType()
        {
            return Type;
        }

        internal string getSequenceNumber()
        {
            return SequenceNumber;
        }

        internal string getUpgradedPersistedProperties()
        {
            return UpgradedPersistedProperties;
        }
    }
}
