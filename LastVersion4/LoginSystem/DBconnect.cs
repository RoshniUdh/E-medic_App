using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginSystem
{
    class DBconnect
    {


        // Data Checker, kijkt of de email al gebruikt word
        static public bool CheckForAvailableEmail(List<string> dbEmailList, string email)
        {
            foreach (string dbEmail in dbEmailList)
            {
                if (dbEmail.Trim().ToLower().Equals(email.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }


        //Leest de database data
        static public DataTable GrabData(string statement)
        {

            DataTable dataStore = new DataTable();
            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");

            SqlCommand sqlStatement = new SqlCommand(statement, connection);
            SqlDataAdapter dataGrabber = new SqlDataAdapter(sqlStatement);


            connection.Open();
            dataGrabber.Fill(dataStore);
            connection.Close();

            return dataStore;
        }



        // Create een account en geeft een Bool terug of de email bezet is
        static public void Create_Account()
        {



            DataTable data = GrabData("select * from  Gebruiker");

            var emailList = new List<string>();
            foreach (DataRow row in data.Rows)
            {




                var EmailObject = row[1];
                if (EmailObject != null)
                {
                    emailList.Add(EmailObject.ToString());
                }
                //var wachtwoord = row[2];
                //var achternaam = row[3];
                //var bloedtype = row[4];
                //var leeftijd = row[5];
                //var geboortedatum = row[6];
                //var aandoeing = row[7];
                var Data = Tuple.Create(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);




            }



            //bool available = CheckForAvailableEmail(emailList, email);

        }
        //profiel data
        static public List<string> Get_Profile(string email)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");



            string strSelect = "";
            strSelect = "select naam,achternaam,email,bloedtype from gebruiker where (email = @Title)";

            SqlCommand cmdSelectID = new SqlCommand(strSelect, connection);
            SqlDataAdapter dataGrabber = new SqlDataAdapter(cmdSelectID);
            cmdSelectID.Parameters.Add("@Title", SqlDbType.VarChar).Value = email;
            connection.Open();
            dataGrabber.Fill(dataStore);
            connection.Close();

            List<string> Profiel = new List<string>();

            foreach (DataRow row in dataStore.Rows)
            {
                var naam = row[0];
                var achternaam = row[1];
                var Email = row[2];
                var bloed = row[3];
                Profiel.Add(naam.ToString().Trim());
                Profiel.Add(achternaam.ToString().Trim());
                Profiel.Add(Email.ToString().Trim());
                Profiel.Add(bloed.ToString().Trim());

            }
            Console.WriteLine(Profiel[0]);
            Console.ReadLine();
            return Profiel;
        }


        //Create een melding
        /*static public void Create_Melding()
        {
            Console.WriteLine("Insert tijd: ");
            String tijd = Console.ReadLine();

            Console.WriteLine("Insert datum: ");
            String datum = Console.ReadLine();

            Console.WriteLine("Insert email: ");
            String email = Console.ReadLine();
            var medicijn = 'C';
            PushDataMelding(tijd, datum, email,medicijn);
        }*/
        //Get a list with a conn string, a = row with a select statement
        static public List<string> GetList(string Statement, int a, string email)
        {
            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");



            string strSelect;
            strSelect = Statement;

            SqlCommand cmdSelectID = new SqlCommand(strSelect, connection);
            cmdSelectID.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            SqlDataAdapter dataGrabber = new SqlDataAdapter(cmdSelectID);
            connection.Open();
            dataGrabber.Fill(dataStore);
            connection.Close();

            var DataList = new List<string>();
            foreach (DataRow row in dataStore.Rows)
            {
                var EmailObject = row[a];

                DataList.Add(EmailObject.ToString());

            }
            return DataList;
        }
        //Pushed de data van Create_Account naar de database
        static public void PushDataGebruiker(string naam, string email, string wachtwoord, string achternaam, string bloedtype, int leeftijd, string geboortedatum, string ziekte)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Gebruiker(naam,email,wachtwoord,achternaam,bloedtype,leeftijd,geboortedatum,ziekte)values(@naam,@email,@wachtwoord,@achternaam,@bloedtype,@leeftijd,@geboortedatum,@ziekte)", connection);
            Console.Write("Entering the Id's.....");
            cmd.Parameters.Add("@naam", SqlDbType.Char, 30).Value = naam;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;
            cmd.Parameters.Add("@wachtwoord", SqlDbType.VarChar, 200).Value = wachtwoord;
            cmd.Parameters.Add("@achternaam", SqlDbType.Char, 45).Value = achternaam;
            cmd.Parameters.Add("@bloedtype", SqlDbType.Char, 30).Value = bloedtype;
            cmd.Parameters.Add("@leeftijd", SqlDbType.Int).Value = leeftijd;
            cmd.Parameters.Add("@geboortedatum", SqlDbType.Date).Value = geboortedatum;
            cmd.Parameters.Add("@ziekte", SqlDbType.VarChar, 200).Value = ziekte;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();


        }
        static public void PushDataAfspraken(string email, string datum, string tijd, string beschrijving)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into afspraken(email,date,tijd,beschrijving)values(@email,@date,@tijd,@beschrijving)", connection);
            Console.Write("Entering the Id's:");
            cmd.Parameters.Add("@tijd", SqlDbType.Time).Value = tijd;
            cmd.Parameters.Add("@datum", SqlDbType.Date).Value = datum;
            cmd.Parameters.Add("@beschrijving", SqlDbType.VarChar, 45).Value = beschrijving;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();

        }
        static public void PushDataMelding(string tijd, string email, int ringtoneid, string medicijn)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Melding(tijd,email,ringtoneid,medicijn)values(@tijd,@email,@ringtoneid,@medicijn)", connection);
            Console.Write("Entering the Id's:");
            cmd.Parameters.Add("@tijd", SqlDbType.Time).Value = tijd;
            cmd.Parameters.Add("@email", SqlDbType.Char, 50).Value = email;
            cmd.Parameters.Add("@ringtoneid", SqlDbType.Int).Value = ringtoneid;
            cmd.Parameters.Add("@medicijn", SqlDbType.Char, 45).Value = medicijn;
            


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close(); ;

        }
        static public void PushDataAccount(string naam, string email, string wachtwoord, string achternaam, int leeftijd,string bloedtype)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");





            SqlCommand cmd = new SqlCommand("Insert into Gebruiker(naam,email,wachtwoord,achternaam,leeftijd,bloedtype)values(@naam,@email,@wachtwoord,@achternaam,@leeftijd,@bloedtype)", connection);
            Console.Write("Entering the Id's.....");
            cmd.Parameters.Add("@naam", SqlDbType.Char, 30).Value = naam;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;
            cmd.Parameters.Add("@wachtwoord", SqlDbType.VarChar, 200).Value = wachtwoord;
            cmd.Parameters.Add("@achternaam", SqlDbType.Char, 45).Value = achternaam;
            cmd.Parameters.Add("@leeftijd", SqlDbType.Int, 7).Value = leeftijd;
            cmd.Parameters.Add("@bloedtype", SqlDbType.VarChar).Value = bloedtype;


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Record Inserted Successfully");
            }
            else
            {
                Console.WriteLine("Operation Failed,Please Try Again Later");
            }
            connection.Close();
        }
        static public void PushDataMedicijn(string naam, int voorraad, string bijsluiter, int dosering, string afbeelding, string email)
         {

             DataTable dataStore = new DataTable();

             SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                          "Database=E-medic;Persist Security Info=True;" +
                                                          "User ID=sa; Password=Teemo1999");





             SqlCommand cmd = new SqlCommand("Insert into Medicijn(naam,voorraad,bijsluiter,dosering,afbeelding,email)values(@naam,@voorraad,@bijsluiter,@dosering,@afbeelding,@email)", connection);
             Console.Write("Entering the Id's.....");
             cmd.Parameters.Add("@naam", SqlDbType.Char, 30).Value = naam;
             cmd.Parameters.Add("@email", SqlDbType.VarChar, 45).Value = email;
             cmd.Parameters.Add("@voorraad", SqlDbType.Int).Value = voorraad;
             cmd.Parameters.Add("@bijsluiter", SqlDbType.VarChar, 255).Value = bijsluiter;
             cmd.Parameters.Add("@dosering", SqlDbType.Int).Value = dosering;
             cmd.Parameters.Add("@afbeelding", SqlDbType.VarChar,255).Value = afbeelding;


             if (connection.State == ConnectionState.Closed)
             {
                 connection.Open();
             }
             int i = cmd.ExecuteNonQuery();
             if (i > 0)
             {
                 Console.WriteLine("Record Inserted Successfully");
             }
             else
             {
                 Console.WriteLine("Operation Failed,Please Try Again Later");
             }
             connection.Close();


         }
        static public bool PullDataLoginAndCompare(string email, string wachtwoord)
        {

            DataTable dataStore = new DataTable();

            SqlConnection connection = new SqlConnection("Data Source=145.24.222.151,8080;" +
                                                         "Database=E-medic;Persist Security Info=True;" +
                                                         "User ID=sa; Password=Teemo1999");


            wachtwoord = wachtwoord + "";
            object ID;

            string strSelect = "";
            strSelect = "select wachtwoord from gebruiker where (email = @Title)";

            SqlCommand cmdSelectID = new SqlCommand(strSelect, connection);
            cmdSelectID.Parameters.Add("@Title", SqlDbType.VarChar).Value = email;
            connection.Open();
            ID = cmdSelectID.ExecuteScalar();
            Console.Write("Entering the Id's:");


            ID = ID + "";

            connection.Close();

            Console.WriteLine(ID);
            if (ID.Equals(wachtwoord))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }

}