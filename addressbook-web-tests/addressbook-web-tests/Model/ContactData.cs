﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string personalBlock;
        private string phoneBlock;
        private string emailBlock;
        private string summaryBlock;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
            if(Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if(Object.ReferenceEquals(this, other))
            {
            return true;
        }

            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; } 
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string BirthDay { get; set; } = "";
        public string BirthMonth { get; set; } = "-";
        public string BirthYear { get; set; } = "";
        public string AnniversaryDay { get; set; } = "-";
        public string AnniversaryMonth { get; set; } = "-";
        public string AnniversaryYear { get; set; } = "";
        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }
        public string AllPhones 
        { 
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work) + CleanUp(Phone2)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails 
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    string res = "";

                    if (Email != "")
                    {
                        res += Email;
                    }
                    if (Email2 != "")
                    {
                        res = res + "\r\n" + Email2;
                    }
                    if (Email3 != "")
                    {
                        res = res + "\r\n" + Email3;
                    }

                    return res.Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }


        public string PersonalBlock
        {
            get
            {
                if (personalBlock != null)
                {
                    return personalBlock;
                }
                else
                {
                    if (Middlename != "")
                    {
                        return (Firstname + " " + Middlename + " " + Lastname
                        + Nickname
                        + Title
                        + Company
                        + Address).Trim();
                    }
                    else
                    {
                        return (Firstname + " " + Lastname
                        + Nickname
                        + Title
                        + Company
                        + Address).Trim();
                    }
                    
                }
            }
            set
            {
                personalBlock = value;
            }
        }

        public string PhoneBlock
        {
            get
            {
                if (phoneBlock != null)
                {
                    return phoneBlock;
                }
                else
                {
                    return ("H: " + Home
                        + "M: " + Mobile
                        + "W: " + Work 
                        + "F: " + Fax).Trim();
                }
            }
            set
            {
                phoneBlock = value;
            }
        }

        public string EmailBlock
        {
            get
            {
                if (emailBlock != null)
                {
                    return emailBlock;
                }
                else
                {
                    return (Email 
                        + Email2
                        + Email3
                        + "Homepage:" + Homepage).Trim();
                }
            }
            set
            {
                emailBlock = value;
            }
        }

        public string SummaryBlock
        {
            get
            {
                if (summaryBlock != null)
                {
                    return summaryBlock;
                }
                else
                {
                    return (Address2
                        + "P: " + Phone2
                        + Notes).Trim();
                }
            }
            set
            {
                summaryBlock = value;
            }
        }
    }
}
