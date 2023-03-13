using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfOsztalyzas
{
    public class Grading
    {
        private String name;
        private String date;
        private String subject;
        private int grade;
        private String firstName;
        private String lastName;
        public string Name => name;
        public string Date => date; 
        public string Subject => subject;
        public int Grade => grade;

        public string FamilyName => name.Split(" ")[0];
        public Grading(string name, string date, string subject, int grade)
        {
            this.name = name;
            this.date = date;
            this.subject = subject;
            this.grade = grade;
        }

        static string ReverseNames(string name)
        {
            string firstName;
            string lastName;

            firstName = name.Split(" ")[0];
            lastName = name.Split(" ")[1];

            return $"{lastName} + {firstName}";
        }

    }
        //todo Bővítse az osztályt! Készítsen CsaladiNev néven property-t, ami a névből a családi nevet adja vissza. Feltételezve, hogy a névnek csak az első tagja az.

        //todo Készítsen metódust ForditottNev néven, ami a két tagból álló nevek esetén megfordítja a névtagokat. Pld. Kiss Ádám => Ádám Kiss
}
