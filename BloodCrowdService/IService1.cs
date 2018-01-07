using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BloodCrowdService
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators")]
        [Description("Return all donators")]
        List<Donator> GetDonators();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donatorsshort")]
        [Description("Return a short info of all donators")]
        List<ShortDonator> GetDonatorsShort();


        // XML NAO SAI DO SERVIÇO! Tudo o que passamos é objetos de classes! 

        // Adicionar dador

        [OperationContract]
        //deve devolver true
        [WebInvoke(Method = "POST", UriTemplate = "/donator")]
        [Description("Posts a donator at xml file")]
        bool AddNewDonator(Donator bd);

        // Remover Dador

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/donator/RemoveDonator/{id}")]
        [Description("Removes a donator/list of donators at xml file")]
        bool RemoverDonator(string id);

        // Procurar Dador especifico Por Nome, Idade, Grupo Sanguineo, Compatibilidade, IMC  ( SO PARA ANDROID) 

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donatorsnumber/{number}")]
        [Description("Return donators by number")]
        List<Donator> GetDonatorsByNumber(string number);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/{nome}")] 
        [Description("Return donators by name")]
        List<ShortDonator> GetDonatorsByName(string nome);


        // Procurar por idade
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/getByAge/{age}")]
        [Description("Return donators by age")]
        List<ShortDonator> GetDonatorsByAge(string age);


        // Procurar por grupo sanguineo
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/getByBloodType/{bloodType}")]
        [Description("Return donators by bloodtype")]
        List<ShortDonator> GetDonatorsByBloodType(string bloodType);

        //Procurar por compatibilidade de grupo sanguineo
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/getByBloodTypeCompatibility/{bloodType}")]
        [Description("Return donators by bloodtype compatibility")]
        List<ShortDonator> GetDonatorsByBloodTypeCompatibility(string bloodType);

        //Procurar por Imc menor ou igual
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/getByImcMenorOuIgual/{imc}")]
        [Description("Return donators by imc less or equal")]
        List<ShortDonator> GetDonatorsByImcMenorOuIgual(string imc);


        //Procurar por IMC maior ou igual
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/donators/getByImcMaiorOuIgual/{imc}")]
        [Description("Return donators by imc equal or less")]
        List<ShortDonator> GetDonatorsByImcMaiorOuIgual(string imc);

        //Calcular IMC
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate =  "/donator/CalcularIMC/{peso}/{altura}")]
        [Description("Calcuates a IMC ")]
        double CalcularImc(string peso, string altura);




        // TODO: Adicione suas operações de serviço aqui
    }


    // Use um contrato de dados como ilustrado no exemplo abaixo para adicionar tipos compostos a operações de serviço.

    [DataContract]
    public class ShortDonator
    {
        public ShortDonator(int number, String firstName, String lastName, String bloodType)
        {
            this.Number = number;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BloodType = bloodType;
        }

        [DataMember]
        public int Number { get; private set; }

        [DataMember]
        public String FirstName { get; private set; }

        [DataMember]
        public String LastName { get; private set; }

        [DataMember]
        public String BloodType { get; private set; }

    }


    [DataContract]
    public class Donator
    {
        public Donator(int number, String sexo, String firstName, String lastName, String streetAddress,
                String city, String statefull, String zipCode, String eMail, String userName, String password,
                long telephoneNumber,
                String mothersMaiden, String birthDay, int age, String occupation, String company, String vehicle,
                String bloodType,
                double kilograms, double centimeters, String guid, String latitude, String longitude, double imc)
        {
            this.Number = number;
            this.Sexo = sexo;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.StreetAddress = streetAddress;
            this.City = city;
            this.Statefull = statefull;
            this.ZipCode = zipCode;
            this.EMail = eMail;
            this.UserName = userName;
            this.Password = password;
            this.TelephoneNumber = telephoneNumber;
            this.MothersMaiden = mothersMaiden;
            this.BirthDay = birthDay;
            this.Age = age;
            this.Occupation = occupation;
            this.Company = company;
            this.Vehicle = vehicle;
            this.BloodType = bloodType;
            this.Kilograms = kilograms;
            this.Centimeters = centimeters;
            this.Guid = guid;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Imc = imc;

        }
        [DataMember]
        public int Number { get; private set; }

        [DataMember]
        public String Sexo { get; private set; }

        [DataMember]
        public String FirstName { get; private set; }

        [DataMember]
        public String LastName { get; private set; }

        [DataMember]
        public String StreetAddress { get; private set; }

        [DataMember]
        public String City { get; private set; }

        [DataMember]
        public String Statefull { get; private set; }

        [DataMember]
        public String ZipCode { get; private set; }

        [DataMember]
        public String EMail { get; private set; }

        [DataMember]
        public String UserName { get; private set; }

        [DataMember]
        public String Password { get; private set; }

        [DataMember]
        public long TelephoneNumber { get; private set; }

        [DataMember]
        public String MothersMaiden { get; private set; }

        [DataMember]
        public String BirthDay { get; private set; }

        [DataMember]
        public int Age { get; private set; }

        [DataMember]
        public String Occupation { get; private set; }

        [DataMember]
        public String Company { get; private set; }

        [DataMember]
        public String Vehicle { get; private set; }

        [DataMember]
        public String BloodType { get; private set; }

        [DataMember]
        public double Kilograms { get; private set; }

        [DataMember]
        public double Centimeters { get; private set; }

        [DataMember]
        public String Guid { get; private set; }

        [DataMember]
        public String Latitude { get; private set; }

        [DataMember]
        public String Longitude { get; private set; }

        [DataMember]
        public double Imc { get; private set; }

        public override string ToString()
        {
            string bd = string.Empty;
            bd += "Number: " + Number + Environment.NewLine;
            bd += "Sex: " + Sexo + Environment.NewLine;
            bd += "First Name: " + FirstName + Environment.NewLine;
            bd += "Last Name: " + LastName + Environment.NewLine;
            bd += "Street Address: " + StreetAddress + Environment.NewLine;
            bd += "City: " + City + Environment.NewLine;
            bd += "Statefull: " + Statefull + Environment.NewLine;
            bd += "ZipCode: " + ZipCode + Environment.NewLine;
            bd += "EMail: " + EMail + Environment.NewLine;
            bd += "UserName: " + UserName + Environment.NewLine;
            bd += "Password: " + Password + Environment.NewLine;
            bd += "TelephoneNumber: " + TelephoneNumber + Environment.NewLine;
            bd += "MothersMaiden: " + MothersMaiden + Environment.NewLine;
            bd += "BirthDay: " + BirthDay + Environment.NewLine;
            bd += "Age: " + Age + Environment.NewLine;
            bd += "Occupation: " + Occupation + Environment.NewLine;
            bd += "Company: " + Company + Environment.NewLine;
            bd += "Vehicle: " + Vehicle + Environment.NewLine;
            bd += "BloodType: " + BloodType + Environment.NewLine;
            bd += "Kilograms: " + Kilograms + Environment.NewLine;
            bd += "Centimeters: " + Centimeters + Environment.NewLine;
            bd += "Guid: " + Guid + Environment.NewLine;
            bd += "Latitude: " + Latitude + Environment.NewLine;
            bd += "Longitude: " + Longitude + Environment.NewLine;
            bd += "Imc: " + Imc + Environment.NewLine;

            return bd;
        }
    }
}
