using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.DynamicData;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;

namespace BloodCrowdService
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Service1 : IService1
    {
        private static string FILEPATH;
        public Service1()
        {
            FILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "BaseDados.xml");
        }

        public List<Donator> GetDonators()
        {

            List<Donator> listDonators = new List<Donator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            IFormatProvider cultureint = new System.Globalization.CultureInfo("pt-PT", true);


            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    int id = Convert.ToInt32(dm.Attribute("id").Value);
                    String sexo = dm.Element("Sexo").Value;
                    String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                    String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                    String rua = dm.Element("Rua").Value;
                    String cidade = dm.Element("Cidade").Value;
                    String distrito = dm.Element("Distrito").Value;
                    String codigo_postal = dm.Element("Codigo_Postal").Value;
                    String email = dm.Element("Email").Value;
                    String username = dm.Element("Username").Value;
                    String password = dm.Element("Password").Value;
                    long telefone = Convert.ToInt64(dm.Element("Telefone").Value);
                    String nome_mae = dm.Element("Nome_da_mae").Value;
                    DateTime data_nascimento = DateTime.Parse(dm.Element("Data_Nascimento").Value, culture, DateTimeStyles.AssumeLocal);
                    String dn = Convert.ToString(data_nascimento);
                    int idade = Convert.ToInt32(dm.Element("Idade").Value);
                    String ocupacao = dm.Element("Ocupaçao").Value;
                    String empresa = dm.Element("Empresa").Value;
                    String veiculo = dm.Element("Veiculo").Value;
                    String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;
                    double peso = Convert.ToDouble(dm.Element("Peso").Value, cultureint);
                    double altura = Convert.ToDouble(dm.Element("Altura").Value);
                    String guid = dm.Element("GUID").Value;
                    String latitude = dm.Element("Latitude").Value;
                    String longitude = dm.Element("Longitude").Value;
                    double IMC = CalcularImc(Convert.ToString(peso), Convert.ToString(altura));


                    listDonators.Add(new Donator(id, sexo, primeiro_nome, ultimo_nome, rua, cidade, distrito, codigo_postal, email, username,
                        password, telefone, nome_mae, dn, idade, ocupacao, empresa, veiculo, tipo_sangue, peso, altura,
                        guid, latitude, longitude, IMC));

                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsShort()
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            try
            {
                

                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    int id = Convert.ToInt32(dm.Attribute("id").Value);                
                    String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                    String ultimo_nome = dm.Element("Ultimo_Nome").Value;                                    
                    String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;
                 

                    listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));

                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }



        public bool RemoverDonator(string id)
        {
            bool sucesso = true;
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(FILEPATH);
                XmlNode node = doc.SelectSingleNode("//Donator[@id=" + id + "]");
                XmlNode root = doc.DocumentElement;
                root.RemoveChild(node);
                doc.Save(FILEPATH);
            }
            catch (Exception e)
            {
                sucesso = false;
            }
            return sucesso;
        }

        public bool AddNewDonator(Donator bd)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(FILEPATH);
            XmlNode root = doc.DocumentElement;
            XmlNode id = root.SelectSingleNode("Donator[last()]/@id");
            int idU = Convert.ToInt32(id.InnerText);
            String idUltimo = Convert.ToString(idU + 1);

            XmlElement donator = doc.CreateElement("Donator");
            donator.SetAttribute("id", idUltimo);
           

            XmlElement genero = doc.CreateElement("Sexo");
            donator.AppendChild(genero);
            genero.InnerText = bd.Sexo;

            XmlElement primeiroNome = doc.CreateElement("Primeiro_Nome");
            donator.AppendChild(primeiroNome);
            primeiroNome.InnerText = bd.FirstName;

            XmlElement ultimoNome = doc.CreateElement("Ultimo_Nome");
            donator.AppendChild(ultimoNome);
            ultimoNome.InnerText = bd.LastName;

            XmlElement rua = doc.CreateElement("Rua");
            donator.AppendChild(rua);
            rua.InnerText = bd.StreetAddress;

            XmlElement cidade = doc.CreateElement("Cidade");
            donator.AppendChild(cidade);
            cidade.InnerText = bd.City;

            XmlElement distrito = doc.CreateElement("Distrito");
            donator.AppendChild(distrito);
            distrito.InnerText = bd.Statefull;

            XmlElement codigoPostal = doc.CreateElement("Codigo_Postal");
            donator.AppendChild(codigoPostal);
            codigoPostal.InnerText = bd.ZipCode;

            XmlElement mail = doc.CreateElement("Email");
            donator.AppendChild(mail);
            mail.InnerText = bd.EMail;

            XmlElement userN = doc.CreateElement("Username");
            donator.AppendChild(userN);
            userN.InnerText = bd.UserName;

            XmlElement pwd = doc.CreateElement("Password");
            donator.AppendChild(pwd);
            pwd.InnerText = bd.Password;

            XmlElement telefone = doc.CreateElement("Telefone");
            donator.AppendChild(telefone);
            telefone.InnerText = Convert.ToString(bd.TelephoneNumber);

            XmlElement nomeDaMae = doc.CreateElement("Nome_da_mae");
            donator.AppendChild(nomeDaMae);
            nomeDaMae.InnerText = bd.MothersMaiden;

            XmlElement dataNasc = doc.CreateElement("Data_Nascimento");
            donator.AppendChild(dataNasc);
            dataNasc.InnerText = bd.BirthDay;

            XmlElement idade = doc.CreateElement("Idade");
            donator.AppendChild(idade);
            idade.InnerText = Convert.ToString(bd.Age);

            XmlElement ocupacao = doc.CreateElement("Ocupaçao");
            donator.AppendChild(ocupacao);
            ocupacao.InnerText = bd.Occupation;

            XmlElement empresa = doc.CreateElement("Empresa");
            donator.AppendChild(empresa);
            empresa.InnerText = bd.Company;

            XmlElement veiculo = doc.CreateElement("Veiculo");
            donator.AppendChild(veiculo);
            veiculo.InnerText = bd.Vehicle;

            XmlElement tipoSanguineo = doc.CreateElement("Tipo_Sanguineo");
            donator.AppendChild(tipoSanguineo);
            tipoSanguineo.InnerText = bd.BloodType;

            XmlElement peso = doc.CreateElement("Peso");
            donator.AppendChild(peso);
            peso.InnerText = Convert.ToString(bd.Kilograms);

            XmlElement altura = doc.CreateElement("Altura");
            donator.AppendChild(altura);
            altura.InnerText = Convert.ToString(bd.Centimeters);

            XmlElement guId = doc.CreateElement("GUID");
            donator.AppendChild(guId);
            guId.InnerText = bd.Guid;

            XmlElement lat = doc.CreateElement("Latitude");
            donator.AppendChild(lat);
            lat.InnerText = bd.Latitude;

            XmlElement lon = doc.CreateElement("Longitude");
            donator.AppendChild(lon);
            lon.InnerText = bd.Longitude;

            root.AppendChild(donator);
            doc.Save(FILEPATH);


            return true;
        }

        public List<Donator> GetDonatorsByNumber(string number)
        {

            List<Donator> listDonators = new List<Donator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    IFormatProvider cultureint = new System.Globalization.CultureInfo("pt-PT", true);
                    int comparar = Convert.ToInt32(dm.Attribute("id").Value);
                    
                    if (comparar.Equals(Convert.ToInt32(number)))
                    {

            
                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String sexo = dm.Element("Sexo").Value;
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String rua = dm.Element("Rua").Value;
                        String cidade = dm.Element("Cidade").Value;
                        String distrito = dm.Element("Distrito").Value;
                        String codigo_postal = dm.Element("Codigo_Postal").Value;
                        String email = dm.Element("Email").Value;
                        String username = dm.Element("Username").Value;
                        String password = dm.Element("Password").Value;
                        long telefone = Convert.ToInt64(dm.Element("Telefone").Value);
                        String nome_mae = dm.Element("Nome_da_mae").Value;
                        DateTime data_nascimento = DateTime.Parse(dm.Element("Data_Nascimento").Value, culture, DateTimeStyles.AssumeLocal);
                        String dn = Convert.ToString(data_nascimento);
                        int idade = Convert.ToInt32(dm.Element("Idade").Value);
                        String ocupacao = dm.Element("Ocupaçao").Value;
                        String empresa = dm.Element("Empresa").Value;
                        String veiculo = dm.Element("Veiculo").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;
                        double peso = Convert.ToDouble(dm.Element("Peso").Value, cultureint);
                        double altura = Convert.ToDouble(dm.Element("Altura").Value);
                        String guid = dm.Element("GUID").Value;
                        String latitude = dm.Element("Latitude").Value;
                        String longitude = dm.Element("Longitude").Value;
                        double IMC = CalcularImc(Convert.ToString(peso), Convert.ToString(altura));

                        listDonators.Add(new Donator(id, sexo, primeiro_nome, ultimo_nome, rua, cidade, distrito, codigo_postal, email, username,
                            password, telefone, nome_mae, dn, idade, ocupacao, empresa, veiculo, tipo_sangue, peso, altura,
                            guid, latitude, longitude, IMC));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByName(string nome)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    String comparar = dm.Element("Primeiro_Nome").Value + " " +  dm.Element("Ultimo_Nome").Value; 
                    if (comparar.Equals(nome))
                    {


                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByAge(string age)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    String comparar = dm.Element("Idade").Value;
                    if (comparar.Equals(age))
                    {


                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByBloodType(string bloodType)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    String comparar = dm.Element("Tipo_Sanguineo").Value;
                    if (comparar.Equals(bloodType))
                    {


                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByBloodTypeCompatibility(string bloodType)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            String comp1 = "";
            String comp2 = "";
            String comp3 = "";
            String comp4 = "";
            String comp5 = "";
            String comp6 = "";
            String comp7 = "";
            String comp8 = "";
            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                //Percorre todos os dadores
                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    // Guarda o tipo sanguineo de um dador na string comparar
                    String comparar = dm.Element("Tipo_Sanguineo").Value;


                    // Compara o tipo sanguineo a pesquisar e atribuis os tipos de sangue compativeis de receber as variaveis auxiliares
                    if (bloodType.Equals("A+"))
                    {
                        comp1 = "A+";
                        comp2 = "A-";
                        comp3 = "O-";
                        comp4 = "O+";
                    }
                    else if (bloodType.Equals("A-"))
                    {
                        comp1 = "A-";
                        comp2 = "O-";

                    }
                    else if (bloodType.Equals("B+"))
                    {
                        comp1 = "B+";
                        comp2 = "B-";
                        comp3 = "O-";
                        comp4 = "O+";
                    }
                    else if (bloodType.Equals("AB+"))
                    {
                        comp1 = "AB+";
                        comp2 = "AB-";
                        comp3 = "O-";
                        comp4 = "O+";
                        comp5 = "B+";
                        comp6 = "B-";
                        comp7 = "A-";
                        comp8 = "A+";
                    }
                    else if (bloodType.Equals("AB-"))
                    {
                        comp1 = "AB-";
                        comp2 = "A-";
                        comp3 = "O-";
                        comp4 = "B-";
                    }
                    else if (bloodType.Equals("O+"))
                    {
                        comp1 = "O+";
                        comp2 = "O-";
                    }
                    else if (bloodType.Equals("O-"))
                    {
                        comp1 = "O-"; 
                    }
                    

                    //Compara as variavies auxiliares com o tipo de sangue do dador!
                    if (comp1.Equals(comparar) || comp2.Equals(comparar) || comp3.Equals(comparar) || comp4.Equals(comparar) ||
                    comp5.Equals(comparar) || comp6.Equals(comparar)  || comp7.Equals(comparar)  || comp8.Equals(comparar))
                    {

                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                throw new FaultException(e.ToString());

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByImcMaiorOuIgual(string imc)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();
            IFormatProvider cultureint = new System.Globalization.CultureInfo("pt-PT", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    double peso = Convert.ToDouble(dm.Element("Peso").Value, cultureint);
                    double altura = Convert.ToDouble(dm.Element("Altura").Value, cultureint);
                    double comparar = CalcularImc(Convert.ToString(peso), Convert.ToString(altura));
                    if (Convert.ToDouble(imc) <= comparar)
                    {


                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                

            }

            return listDonators;
        }

        public List<ShortDonator> GetDonatorsByImcMenorOuIgual(string imc)
        {

            List<ShortDonator> listDonators = new List<ShortDonator>();

            IFormatProvider cultureint = new System.Globalization.CultureInfo("pt-PT", true);

            try
            {
                XDocument xdoc = XDocument.Load(FILEPATH);

                foreach (var dm in xdoc.Descendants("Donator"))
                {
                    double peso = Convert.ToDouble(dm.Element("Peso").Value, cultureint);
                    double altura = Convert.ToDouble(dm.Element("Altura").Value, cultureint);
                    double comparar = CalcularImc(Convert.ToString(peso), Convert.ToString(altura));
                    if (Convert.ToDouble(imc) >= comparar)
                    {


                        int id = Convert.ToInt32(dm.Attribute("id").Value);
                        String primeiro_nome = dm.Element("Primeiro_Nome").Value;
                        String ultimo_nome = dm.Element("Ultimo_Nome").Value;
                        String tipo_sangue = dm.Element("Tipo_Sanguineo").Value;


                        listDonators.Add(new ShortDonator(id, primeiro_nome, ultimo_nome, tipo_sangue));
                    }
                }
            }
            catch (Exception e)
            {
                

            }

            return listDonators;
        }

        public double CalcularImc(string peso, string altura)
        {
            IFormatProvider cultureint = new System.Globalization.CultureInfo("pt-PT", true);

            double pesoC = Convert.ToDouble(peso, cultureint);
            double alturaC = Convert.ToDouble(altura, cultureint);
            double imc = 0;
            imc = pesoC / ((alturaC * alturaC) / 10000);
            return imc;
        }
    }
}
