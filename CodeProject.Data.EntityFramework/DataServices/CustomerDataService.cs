using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Account Data Service
    /// </summary>
    public class PersonDataService : EntityFrameworkService, IPersonDataService
    {

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePerson(Person person)
        {

        }

        /// <summary>
        /// Create Person
        /// </summary>
        /// <param name="person"></param>
        public void CreatePerson(Person person)
        {
            dbConnection.Persons.Add(person);
        }

        /// <summary>
        /// Get Person
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public Person GetPerson(int personID)
        {
            Person person = dbConnection.Persons.Where(c => c.PersonID == personID).FirstOrDefault();
            return person;
        }

        /// <summary>
        /// Get Persons
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            totalRows = 0;

            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Persons.Count();

            List<Person> persons = dbConnection.Persons.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();

            return persons;
        }

        public void DeletePerson(Person person)
        {
            dbConnection.Persons.Remove(person);
        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        public void InitializeData()
        {
            List<Product> products = dbConnection.Products.ToList();
            foreach (Product product in products)
            {
                dbConnection.Products.Remove(product);
            }

            List<Person> persons = dbConnection.Persons.ToList();
            foreach (Person person in persons)
            {
                dbConnection.Persons.Remove(person);
            }

        }

        /// <summary>
        /// Load Data
        /// </summary>
        public void LoadData()
        {

            List<string> productInsertStatements = new List<string>();


            productInsertStatements.Add("1,'Chai',1,1,'10 boxes x 20 bags',18,39,0,10,0)");
            productInsertStatements.Add("2,'Chang',1,1,'24 - 12 oz bottles',19,17,40,25,0)");
            productInsertStatements.Add("3,'Aniseed Syrup',1,2,'12 - 550 ml bottles',10,13,70,25,0)");
            productInsertStatements.Add("4,'Chef Anton''s Cajun Seasoning',2,2,'48 - 6 oz jars',22,53,0,0,0)");
            productInsertStatements.Add("5,'Chef Anton''s Gumbo Mix',2,2,'36 boxes',21.35,0,0,0,1)");
            productInsertStatements.Add("6,'Grandma''s Boysenberry Spread',3,2,'12 - 8 oz jars',25,120,0,25,0)");
            productInsertStatements.Add("7,'Uncle Bob''s Organic Dried Pears',3,7,'12 - 1 lb pkgs.',30,15,0,10,0)");
            productInsertStatements.Add("8,'Northwoods Cranberry Sauce',3,2,'12 - 12 oz jars',40,6,0,0,0)");
            productInsertStatements.Add("9,'Mishi Kobe Niku',4,6,'18 - 500 g pkgs.',97,29,0,0,1)");
            productInsertStatements.Add("10,'Ikura',4,8,'12 - 200 ml jars',31,31,0,0,0)");
            productInsertStatements.Add("11,'Queso Cabrales',5,4,'1 kg pkg.',21,22,30,30,0)");
            productInsertStatements.Add("12,'Queso Manchego La Pastora',5,4,'10 - 500 g pkgs.',38,86,0,0,0)");
            productInsertStatements.Add("13,'Konbu',6,8,'2 kg box',6,24,0,5,0)");
            productInsertStatements.Add("14,'Tofu',6,7,'40 - 100 g pkgs.',23.25,35,0,0,0)");
            productInsertStatements.Add("15,'Genen Shouyu',6,2,'24 - 250 ml bottles',15.5,39,0,5,0)");
            productInsertStatements.Add("16,'Pavlova',7,3,'32 - 500 g boxes',17.45,29,0,10,0)");
            productInsertStatements.Add("17,'Alice Mutton',7,6,'20 - 1 kg tins',39,0,0,0,1)");
            productInsertStatements.Add("18,'Carnarvon Tigers',7,8,'16 kg pkg.',62.5,42,0,0,0)");
            productInsertStatements.Add("19,'Teatime Chocolate Biscuits',8,3,'10 boxes x 12 pieces',9.2,25,0,5,0)");
            productInsertStatements.Add("20,'Sir Rodney''s Marmalade',8,3,'30 gift boxes',81,40,0,0,0)");
            productInsertStatements.Add("21,'Sir Rodney''s Scones',8,3,'24 pkgs. x 4 pieces',10,3,40,5,0)");
            productInsertStatements.Add("22,'Gustaf''s Knäckebröd',9,5,'24 - 500 g pkgs.',21,104,0,25,0)");
            productInsertStatements.Add("23,'Tunnbröd',9,5,'12 - 250 g pkgs.',9,61,0,25,0)");
            productInsertStatements.Add("24,'Guaraná Fantástica',10,1,'12 - 355 ml cans',4.5,20,0,0,1)");
            productInsertStatements.Add("25,'NuNuCa Nuß-Nougat-Creme',11,3,'20 - 450 g glasses',14,76,0,30,0)");
            productInsertStatements.Add("26,'Gumbär Gummibärchen',11,3,'100 - 250 g bags',31.23,15,0,0,0)");
            productInsertStatements.Add("27,'Schoggi Schokolade',11,3,'100 - 100 g pieces',43.9,49,0,30,0)");
            productInsertStatements.Add("28,'Rössle Sauerkraut',12,7,'25 - 825 g cans',45.6,26,0,0,1)");
            productInsertStatements.Add("29,'Thüringer Rostbratwurst',12,6,'50 bags x 30 sausgs.',123.79,0,0,0,1)");
            productInsertStatements.Add("30,'Nord-Ost Matjeshering',13,8,'10 - 200 g glasses',25.89,10,0,15,0)");
            productInsertStatements.Add("31,'Gorgonzola Telino',14,4,'12 - 100 g pkgs',12.5,0,70,20,0)");
            productInsertStatements.Add("32,'Mascarpone Fabioli',14,4,'24 - 200 g pkgs.',32,9,40,25,0)");
            productInsertStatements.Add("33,'Geitost',15,4,'500 g',2.5,112,0,20,0)");
            productInsertStatements.Add("34,'Sasquatch Ale',16,1,'24 - 12 oz bottles',14,111,0,15,0)");
            productInsertStatements.Add("35,'Steeleye Stout',16,1,'24 - 12 oz bottles',18,20,0,15,0)");
            productInsertStatements.Add("36,'Inlagd Sill',17,8,'24 - 250 g  jars',19,112,0,20,0)");
            productInsertStatements.Add("37,'Gravad lax',17,8,'12 - 500 g pkgs.',26,11,50,25,0)");
            productInsertStatements.Add("38,'Côte de Blaye',18,1,'12 - 75 cl bottles',263.5,17,0,15,0)");
            productInsertStatements.Add("39,'Chartreuse verte',18,1,'750 cc per bottle',18,69,0,5,0)");
            productInsertStatements.Add("40,'Boston Crab Meat',19,8,'24 - 4 oz tins',18.4,123,0,30,0)");
            productInsertStatements.Add("41,'Jack''s New England Clam Chowder',19,8,'12 - 12 oz cans',9.65,85,0,10,0)");
            productInsertStatements.Add("42,'Singaporean Hokkien Fried Mee',20,5,'32 - 1 kg pkgs.',14,26,0,0,1)");
            productInsertStatements.Add("43,'Ipoh Coffee',20,1,'16 - 500 g tins',46,17,10,25,0)");
            productInsertStatements.Add("44,'Gula Malacca',20,2,'20 - 2 kg bags',19.45,27,0,15,0)");
            productInsertStatements.Add("45,'Rogede sild',21,8,'1k pkg.',9.5,5,70,15,0)");
            productInsertStatements.Add("46,'Spegesild',21,8,'4 - 450 g glasses',12,95,0,0,0)");
            productInsertStatements.Add("47,'Zaanse koeken',22,3,'10 - 4 oz boxes',9.5,36,0,0,0)");
            productInsertStatements.Add("48,'Chocolade',22,3,'10 pkgs.',12.75,15,70,25,0)");
            productInsertStatements.Add("49,'Maxilaku',23,3,'24 - 50 g pkgs.',20,10,60,15,0)");
            productInsertStatements.Add("50,'Valkoinen suklaa',23,3,'12 - 100 g bars',16.25,65,0,30,0)");
            productInsertStatements.Add("51,'Manjimup Dried Apples',24,7,'50 - 300 g pkgs.',53,20,0,10,0)");
            productInsertStatements.Add("52,'Filo Mix',24,5,'16 - 2 kg boxes',7,38,0,25,0)");
            productInsertStatements.Add("53,'Perth Pasties',24,6,'48 pieces',32.8,0,0,0,1)");
            productInsertStatements.Add("54,'Tourtière',25,6,'16 pies',7.45,21,0,10,0)");
            productInsertStatements.Add("55,'Pâté chinois',25,6,'24 boxes x 2 pies',24,115,0,20,0)");
            productInsertStatements.Add("56,'Gnocchi di nonna Alice',26,5,'24 - 250 g pkgs.',38,21,10,30,0)");
            productInsertStatements.Add("57,'Ravioli Angelo',26,5,'24 - 250 g pkgs.',19.5,36,0,20,0)");
            productInsertStatements.Add("58,'Escargots de Bourgogne',27,8,'24 pieces',13.25,62,0,20,0)");
            productInsertStatements.Add("59,'Raclette Courdavault',28,4,'5 kg pkg.',55,79,0,0,0)");
            productInsertStatements.Add("60,'Camembert Pierrot',28,4,'15 - 300 g rounds',34,19,0,0,0)");
            productInsertStatements.Add("61,'Sirop d''érable',29,2,'24 - 500 ml bottles',28.5,113,0,25,0)");
            productInsertStatements.Add("62,'Tarte au sucre',29,3,'48 pies',49.3,17,0,0,0)");
            productInsertStatements.Add("63,'Vegie-spread',7,2,'15 - 625 g jars',43.9,24,0,5,0)");
            productInsertStatements.Add("64,'Wimmers gute Semmelknödel',12,5,'20 bags x 4 pieces',33.25,22,80,30,0)");
            productInsertStatements.Add("65,'Louisiana Fiery Hot Pepper Sauce',2,2,'32 - 8 oz bottles',21.05,76,0,0,0)");
            productInsertStatements.Add("66,'Louisiana Hot Spiced Okra',2,2,'24 - 8 oz jars',17,4,100,20,0)");
            productInsertStatements.Add("67,'Laughing Lumberjack Lager',16,1,'24 - 12 oz bottles',14,52,0,10,0)");
            productInsertStatements.Add("68,'Scottish Longbreads',8,3,'10 boxes x 8 pieces',12.5,6,10,15,0)");
            productInsertStatements.Add("69,'Gudbrandsdalsost',15,4,'10 kg pkg.',36,26,0,15,0)");
            productInsertStatements.Add("70,'Outback Lager',7,1,'24 - 355 ml bottles',15,15,10,30,0)");
            productInsertStatements.Add("71,'Flotemysost',15,4,'10 - 500 g pkgs.',21.5,26,0,0,0)");
            productInsertStatements.Add("72,'Mozzarella di Giovanni',14,4,'24 - 200 g pkgs.',34.8,14,0,0,0)");
            productInsertStatements.Add("73,'Röd Kaviar',17,8,'24 - 150 g jars',15,101,0,5,0)");
            productInsertStatements.Add("74,'Longlife Tofu',4,7,'5 kg pkg.',10,4,20,5,0)");
            productInsertStatements.Add("75,'Rhönbräu Klosterbier',12,1,'24 - 0.5 l bottles',7.75,125,0,25,0)");
            productInsertStatements.Add("76,'Lakkalikööri',23,1,'500 ml',18,57,0,20,0)");
            productInsertStatements.Add("77,'Original Frankfurter grüne Soße',12,2,'12 boxes',13,32,0,15,0)");

            List<Product> products = new List<Product>();

            foreach (string productInsertStatement in productInsertStatements)
            {
                string[] fields = productInsertStatement.Split(',');

                Product product = new Product();
                product.ProductName = fields[1].Replace("'", "");
                product.QuantityPerUnit = fields[4].Replace("'", "");
                product.UnitPrice = Convert.ToDecimal(fields[5].Replace("'", ""));
             
                products.Add(product);

            }

            foreach (Product product in products)
            {
                dbConnection.Products.Add(product);
            }

            List<string> personInsertStatements = new List<string>();

            personInsertStatements.Add("'ALFKI','Alfreds Futterkiste','Maria Anders','Sales Representative','Obere Str. 57','Berlin',NULL,'12209','Germany','030-0074321','030-0076545'");
            personInsertStatements.Add("'ANATR','Ana Trujillo Emparedados y helados','Ana Trujillo','Owner','Avda. de la Constitución 2222','México D.F.',NULL,'05021','Mexico','(5) 555-4729','(5) 555-3745'");
            personInsertStatements.Add("'ANTON','Antonio Moreno Taquería','Antonio Moreno','Owner','Mataderos  2312','México D.F.',NULL,'05023','Mexico','(5) 555-3932',NULL");
            personInsertStatements.Add("'AROUT','Around the Horn','Thomas Hardy','Sales Representative','120 Hanover Sq.','London',NULL,'WA1 1DP','UK','(171) 555-7788','(171) 555-6750'");
            personInsertStatements.Add("'BERGS','Berglunds snabbköp','Christina Berglund','Order Administrator','Berguvsvägen  8','Luleå',NULL,'S-958 22','Sweden','0921-12 34 65','0921-12 34 67'");
            personInsertStatements.Add("'BLAUS','Blauer See Delikatessen','Hanna Moos','Sales Representative','Forsterstr. 57','Mannheim',NULL,'68306','Germany','0621-08460','0621-08924'");
            personInsertStatements.Add("'BLONP','Blondesddsl père et fils','Frédérique Citeaux','Marketing Manager','24, place Kléber','Strasbourg',NULL,'67000','France','88.60.15.31','88.60.15.32'");
            personInsertStatements.Add("'BOLID','Bólido Comidas preparadas','Martín Sommer','Owner','C/ Araquil, 67','Madrid',NULL,'28023','Spain','(91) 555 22 82','(91) 555 91 99'");
            personInsertStatements.Add("'BONAP','Bon app''','Laurence Lebihan','Owner','12, rue des Bouchers','Marseille',NULL,'13008','France','91.24.45.40','91.24.45.41'");
            personInsertStatements.Add("'BOTTM','Bottom-Dollar Markets','Elizabeth Lincoln','Accounting Manager','23 Tsawassen Blvd.','Tsawassen','BC','T2F 8M4','Canada','(604) 555-4729','(604) 555-3745'");
            personInsertStatements.Add("'BSBEV','B''s Beverages','Victoria Ashworth','Sales Representative','Fauntleroy Circus','London',NULL,'EC2 5NT','UK','(171) 555-1212',NULL");
            personInsertStatements.Add("'CACTU','Cactus Comidas para llevar','Patricio Simpson','Sales Agent','Cerrito 333','Buenos Aires',NULL,'1010','Argentina','(1) 135-5555','(1) 135-4892'");
            personInsertStatements.Add("'CENTC','Centro comercial Moctezuma','Francisco Chang','Marketing Manager','Sierras de Granada 9993','México D.F.',NULL,'05022','Mexico','(5) 555-3392','(5) 555-7293'");
            personInsertStatements.Add("'CHOPS','Chop-suey Chinese','Yang Wang','Owner','Hauptstr. 29','Bern',NULL,'3012','Switzerland','0452-076545',NULL");
            personInsertStatements.Add("'COMMI','Comércio Mineiro','Pedro Afonso','Sales Associate','Av. dos Lusíadas, 23','Sao Paulo','SP','05432-043','Brazil','(11) 555-7647',NULL");
            personInsertStatements.Add("'CONSH','Consolidated Holdings','Elizabeth Brown','Sales Representative','Berkeley Gardens 12  Brewery','London',NULL,'WX1 6LT','UK','(171) 555-2282','(171) 555-9199'");
            personInsertStatements.Add("'DRACD','Drachenblut Delikatessen','Sven Ottlieb','Order Administrator','Walserweg 21','Aachen',NULL,'52066','Germany','0241-039123','0241-059428'");
            personInsertStatements.Add("'DUMON','Du monde entier','Janine Labrune','Owner','67, rue des Cinquante Otages','Nantes',NULL,'44000','France','40.67.88.88','40.67.89.89'");
            personInsertStatements.Add("'EASTC','Eastern Connection','Ann Devon','Sales Agent','35 King George','London',NULL,'WX3 6FW','UK','(171) 555-0297','(171) 555-3373'");
            personInsertStatements.Add("'ERNSH','Ernst Handel','Roland Mendel','Sales Manager','Kirchgasse 6','Graz',NULL,'8010','Austria','7675-3425','7675-3426'");
            personInsertStatements.Add("'FAMIA','Familia Arquibaldo','Aria Cruz','Marketing Assistant','Rua Orós, 92','Sao Paulo','SP','05442-030','Brazil','(11) 555-9857',NULL");
            personInsertStatements.Add("'FISSA','FISSA Fabrica Inter. Salchichas S.A.','Diego Roel','Accounting Manager','C/ Moralzarzal, 86','Madrid',NULL,'28034','Spain','(91) 555 94 44','(91) 555 55 93'");
            personInsertStatements.Add("'FOLIG','Folies gourmandes','Martine Rancé','Assistant Sales Agent','184, chaussée de Tournai','Lille',NULL,'59000','France','20.16.10.16','20.16.10.17'");
            personInsertStatements.Add("'FOLKO','Folk och fä HB','Maria Larsson','Owner','Åkergatan 24','Bräcke',NULL,'S-844 67','Sweden','0695-34 67 21',NULL");
            personInsertStatements.Add("'FRANK','Frankenversand','Peter Franken','Marketing Manager','Berliner Platz 43','München',NULL,'80805','Germany','089-0877310','089-0877451'");
            personInsertStatements.Add("'FRANR','France restauration','Carine Schmitt','Marketing Manager','54, rue Royale','Nantes',NULL,'44000','France','40.32.21.21','40.32.21.20'");
            personInsertStatements.Add("'FRANS','Franchi S.p.A.','Paolo Accorti','Sales Representative','Via Monte Bianco 34','Torino',NULL,'10100','Italy','011-4988260','011-4988261'");
            personInsertStatements.Add("'FURIB','Furia Bacalhau e Frutos do Mar','Lino Rodriguez','Sales Manager','Jardim das rosas n. 32','Lisboa',NULL,'1675','Portugal','(1) 354-2534','(1) 354-2535'");
            personInsertStatements.Add("'GALED','Galería del gastrónomo','Eduardo Saavedra','Marketing Manager','Rambla de Cataluña, 23','Barcelona',NULL,'08022','Spain','(93) 203 4560','(93) 203 4561'");
            personInsertStatements.Add("'GODOS','Godos Cocina Típica','José Pedro Freyre','Sales Manager','C/ Romero, 33','Sevilla',NULL,'41101','Spain','(95) 555 82 82',NULL");
            personInsertStatements.Add("'GOURL','Gourmet Lanchonetes','André Fonseca','Sales Associate','Av. Brasil, 442','Campinas','SP','04876-786','Brazil','(11) 555-9482',NULL");
            personInsertStatements.Add("'GREAL','Great Lakes Food Market','Howard Snyder','Marketing Manager','2732 Baker Blvd.','Eugene','OR','97403','USA','(503) 555-7555',NULL");
            personInsertStatements.Add("'GROSR','GROSELLA-Restaurante','Manuel Pereira','Owner','5ª Ave. Los Palos Grandes','Caracas','DF','1081','Venezuela','(2) 283-2951','(2) 283-3397'");
            personInsertStatements.Add("'HANAR','Hanari Carnes','Mario Pontes','Accounting Manager','Rua do Paço, 67','Rio de Janeiro','RJ','05454-876','Brazil','(21) 555-0091','(21) 555-8765'");
            personInsertStatements.Add("'HILAA','HILARION-Abastos','Carlos Hernández','Sales Representative','Carrera 22 con Ave. Carlos Soublette #8-35','San Cristóbal','Táchira','5022','Venezuela','(5) 555-1340','(5) 555-1948'");
            personInsertStatements.Add("'HUNGC','Hungry Coyote Import Store','Yoshi Latimer','Sales Representative','City Center Plaza 516 Main St.','Elgin','OR','97827','USA','(503) 555-6874','(503) 555-2376'");
            personInsertStatements.Add("'HUNGO','Hungry Owl All-Night Grocers','Patricia McKenna','Sales Associate','8 Johnstown Road','Cork','Co. Cork',NULL,'Ireland','2967 542','2967 3333'");
            personInsertStatements.Add("'ISLAT','Island Trading','Helen Bennett','Marketing Manager','Garden House Crowther Way','Cowes','Isle of Wight','PO31 7PJ','UK','(198) 555-8888',NULL");
            personInsertStatements.Add("'KOENE','Königlich Essen','Philip Cramer','Sales Associate','Maubelstr. 90','Brandenburg',NULL,'14776','Germany','0555-09876',NULL");
            personInsertStatements.Add("'LACOR','La corne d''abondance','Daniel Tonini','Sales Representative','67, avenue de l''Europe','Versailles',NULL,'78000','France','30.59.84.10','30.59.85.11'");
            personInsertStatements.Add("'LAMAI','La maison d''Asie','Annette Roulet','Sales Manager','1 rue Alsace-Lorraine','Toulouse',NULL,'31000','France','61.77.61.10','61.77.61.11'");
            personInsertStatements.Add("'LAUGB','Laughing Bacchus Wine Cellars','Yoshi Tannamuri','Marketing Assistant','1900 Oak St.','Vancouver','BC','V3F 2K1','Canada','(604) 555-3392','(604) 555-7293'");
            personInsertStatements.Add("'LAZYK','Lazy K Kountry Store','John Steel','Marketing Manager','12 Orchestra Terrace','Walla Walla','WA','99362','USA','(509) 555-7969','(509) 555-6221'");
            personInsertStatements.Add("'LEHMS','Lehmanns Marktstand','Renate Messner','Sales Representative','Magazinweg 7','Frankfurt a.M.',NULL,'60528','Germany','069-0245984','069-0245874'");
            personInsertStatements.Add("'LETSS','Let''s Stop N Shop','Jaime Yorres','Owner','87 Polk St. Suite 5','San Francisco','CA','94117','USA','(415) 555-5938',NULL");
            personInsertStatements.Add("'LILAS','LILA-Supermercado','Carlos González','Accounting Manager','Carrera 52 con Ave. Bolívar #65-98 Llano Largo','Barquisimeto','Lara','3508','Venezuela','(9) 331-6954','(9) 331-7256'");
            personInsertStatements.Add("'LINOD','LINO-Delicateses','Felipe Izquierdo','Owner','Ave. 5 de Mayo Porlamar','I. de Margarita','Nueva Esparta','4980','Venezuela','(8) 34-56-12','(8) 34-93-93'");
            personInsertStatements.Add("'LONEP','Lonesome Pine Restaurant','Fran Wilson','Sales Manager','89 Chiaroscuro Rd.','Portland','OR','97219','USA','(503) 555-9573','(503) 555-9646'");
            personInsertStatements.Add("'MAGAA','Magazzini Alimentari Riuniti','Giovanni Rovelli','Marketing Manager','Via Ludovico il Moro 22','Bergamo',NULL,'24100','Italy','035-640230','035-640231'");
            personInsertStatements.Add("'MAISD','Maison Dewey','Catherine Dewey','Sales Agent','Rue Joseph-Bens 532','Bruxelles',NULL,'B-1180','Belgium','(02) 201 24 67','(02) 201 24 68'");
            personInsertStatements.Add("'MEREP','Mère Paillarde','Jean Fresnière','Marketing Assistant','43 rue St. Laurent','Montréal','Québec','H1J 1C3','Canada','(514) 555-8054','(514) 555-8055'");
            personInsertStatements.Add("'MORGK','Morgenstern Gesundkost','Alexander Feuer','Marketing Assistant','Heerstr. 22','Leipzig',NULL,'04179','Germany','0342-023176',NULL");
            personInsertStatements.Add("'NORTS','North/South','Simon Crowther','Sales Associate','South House 300 Queensbridge','London',NULL,'SW7 1RZ','UK','(171) 555-7733','(171) 555-2530'");
            personInsertStatements.Add("'OCEAN','Océano Atlántico Ltda.','Yvonne Moncada','Sales Agent','Ing. Gustavo Moncada 8585 Piso 20-A','Buenos Aires',NULL,'1010','Argentina','(1) 135-5333','(1) 135-5535'");
            personInsertStatements.Add("'OLDWO','Old World Delicatessen','Rene Phillips','Sales Representative','2743 Bering St.','Anchorage','AK','99508','USA','(907) 555-7584','(907) 555-2880'");
            personInsertStatements.Add("'OTTIK','Ottilies Käseladen','Henriette Pfalzheim','Owner','Mehrheimerstr. 369','Köln',NULL,'50739','Germany','0221-0644327','0221-0765721'");
            personInsertStatements.Add("'PARIS','Paris spécialités','Marie Bertrand','Owner','265, boulevard Charonne','Paris',NULL,'75012','France','(1) 42.34.22.66','(1) 42.34.22.77'");
            personInsertStatements.Add("'PERIC','Pericles Comidas clásicas','Guillermo Fernández','Sales Representative','Calle Dr. Jorge Cash 321','México D.F.',NULL,'05033','Mexico','(5) 552-3745','(5) 545-3745'");
            personInsertStatements.Add("'PICCO','Piccolo und mehr','Georg Pipps','Sales Manager','Geislweg 14','Salzburg',NULL,'5020','Austria','6562-9722','6562-9723'");
            personInsertStatements.Add("'PRINI','Princesa Isabel Vinhos','Isabel de Castro','Sales Representative','Estrada da saúde n. 58','Lisboa',NULL,'1756','Portugal','(1) 356-5634',NULL");
            personInsertStatements.Add("'QUEDE','Que Delícia','Bernardo Batista','Accounting Manager','Rua da Panificadora, 12','Rio de Janeiro','RJ','02389-673','Brazil','(21) 555-4252','(21) 555-4545'");
            personInsertStatements.Add("'QUEEN','Queen Cozinha','Lúcia Carvalho','Marketing Assistant','Alameda dos Canàrios, 891','Sao Paulo','SP','05487-020','Brazil','(11) 555-1189',NULL");
            personInsertStatements.Add("'QUICK','QUICK-Stop','Horst Kloss','Accounting Manager','Taucherstraße 10','Cunewalde',NULL,'01307','Germany','0372-035188',NULL");
            personInsertStatements.Add("'RANCH','Rancho grande','Sergio Gutiérrez','Sales Representative','Av. del Libertador 900','Buenos Aires',NULL,'1010','Argentina','(1) 123-5555','(1) 123-5556'");
            personInsertStatements.Add("'RATTC','Rattlesnake Canyon Grocery','Paula Wilson','Assistant Sales Representative','2817 Milton Dr.','Albuquerque','NM','87110','USA','(505) 555-5939','(505) 555-3620'");
            personInsertStatements.Add("'REGGC','Reggiani Caseifici','Maurizio Moroni','Sales Associate','Strada Provinciale 124','Reggio Emilia',NULL,'42100','Italy','0522-556721','0522-556722'");
            personInsertStatements.Add("'RICAR','Ricardo Adocicados','Janete Limeira','Assistant Sales Agent','Av. Copacabana, 267','Rio de Janeiro','RJ','02389-890','Brazil','(21) 555-3412',NULL");
            personInsertStatements.Add("'RICSU','Richter Supermarkt','Michael Holz','Sales Manager','Grenzacherweg 237','Genève',NULL,'1203','Switzerland','0897-034214',NULL");
            personInsertStatements.Add("'ROMEY','Romero y tomillo','Alejandra Camino','Accounting Manager','Gran Vía, 1','Madrid',NULL,'28001','Spain','(91) 745 6200','(91) 745 6210'");
            personInsertStatements.Add("'SANTG','Santé Gourmet','Jonas Bergulfsen','Owner','Erling Skakkes gate 78','Stavern',NULL,'4110','Norway','07-98 92 35','07-98 92 47'");
            personInsertStatements.Add("'SAVEA','Save-a-lot Markets','Jose Pavarotti','Sales Representative','187 Suffolk Ln.','Boise','ID','83720','USA','(208) 555-8097',NULL");
            personInsertStatements.Add("'SEVES','Seven Seas Imports','Hari Kumar','Sales Manager','90 Wadhurst Rd.','London',NULL,'OX15 4NB','UK','(171) 555-1717','(171) 555-5646'");
            personInsertStatements.Add("'SIMOB','Simons bistro','Jytte Petersen','Owner','Vinbæltet 34','Kobenhavn',NULL,'1734','Denmark','31 12 34 56','31 13 35 57'");
            personInsertStatements.Add("'SPECD','Spécialités du monde','Dominique Perrier','Marketing Manager','25, rue Lauriston','Paris',NULL,'75016','France','(1) 47.55.60.10','(1) 47.55.60.20'");
            personInsertStatements.Add("'SPLIR','Split Rail Beer & Ale','Art Braunschweiger','Sales Manager','P.O. Box 555','Lander','WY','82520','USA','(307) 555-4680','(307) 555-6525'");
            personInsertStatements.Add("'SUPRD','Suprêmes délices','Pascale Cartrain','Accounting Manager','Boulevard Tirou, 255','Charleroi',NULL,'B-6000','Belgium','(071) 23 67 22 20','(071) 23 67 22 21'");
            personInsertStatements.Add("'THEBI','The Big Cheese','Liz Nixon','Marketing Manager','89 Jefferson Way Suite 2','Portland','OR','97201','USA','(503) 555-3612',NULL");
            personInsertStatements.Add("'THECR','The Cracker Box','Liu Wong','Marketing Assistant','55 Grizzly Peak Rd.','Butte','MT','59801','USA','(406) 555-5834','(406) 555-8083'");
            personInsertStatements.Add("'TOMSP','Toms Spezialitäten','Karin Josephs','Marketing Manager','Luisenstr. 48','Münster',NULL,'44087','Germany','0251-031259','0251-035695'");
            personInsertStatements.Add("'TORTU','Tortuga Restaurante','Miguel Angel Paolino','Owner','Avda. Azteca 123','México D.F.',NULL,'05033','Mexico','(5) 555-2933',NULL");
            personInsertStatements.Add("'TRADH','Tradição Hipermercados','Anabela Domingues','Sales Representative','Av. Inês de Castro, 414','Sao Paulo','SP','05634-030','Brazil','(11) 555-2167','(11) 555-2168'");
            personInsertStatements.Add("'TRAIH','Trail''s Head Gourmet Provisioners','Helvetius Nagy','Sales Associate','722 DaVinci Blvd.','Kirkland','WA','98034','USA','(206) 555-8257','(206) 555-2174'");
            personInsertStatements.Add("'VAFFE','Vaffeljernet','Palle Ibsen','Sales Manager','Smagsloget 45','Århus',NULL,'8200','Denmark','86 21 32 43','86 22 33 44'");
            personInsertStatements.Add("'VICTE','Victuailles en stock','Mary Saveley','Sales Agent','2, rue du Commerce','Lyon',NULL,'69004','France','78.32.54.86','78.32.54.87'");
            personInsertStatements.Add("'VINET','Vins et alcools Chevalier','Paul Henriot','Accounting Manager','59 rue de l''Abbaye','Reims',NULL,'51100','France','26.47.15.10','26.47.15.11'");
            personInsertStatements.Add("'WANDK','Die Wandernde Kuh','Rita Müller','Sales Representative','Adenauerallee 900','Stuttgart',NULL,'70563','Germany','0711-020361','0711-035428'");
            personInsertStatements.Add("'WARTH','Wartian Herkku','Pirkko Koskitalo','Accounting Manager','Torikatu 38','Oulu',NULL,'90110','Finland','981-443655','981-443655'");
            personInsertStatements.Add("'WELLI','Wellington Importadora','Paula Parente','Sales Manager','Rua do Mercado, 12','Resende','SP','08737-363','Brazil','(14) 555-8122',NULL");
            personInsertStatements.Add("'WHITC','White Clover Markets','Karl Jablonski','Owner','305 - 14th Ave. S. Suite 3B','Seattle','WA','98128','USA','(206) 555-4112','(206) 555-4115'");
            personInsertStatements.Add("'WILMK','Wilman Kala','Matti Karttunen','Owner/Marketing Assistant','Keskuskatu 45','Helsinki',NULL,'21240','Finland','90-224 8858','90-224 8858'");
            personInsertStatements.Add("'WOLZA','Wolski  Zajazd','Zbyszek Piestrzeniewicz','Owner','ul. Filtrowa 68','Warszawa',NULL,'01-012','Poland','(26) 642-7012','(26) 642-7012'");

            List<Person> persons = new List<Person>();

            foreach (string personInsertStatement in personInsertStatements)
            {
                string[] fields = personInsertStatement.Split(',');
                string companyName = fields[1].Replace("'", "");
                string contactName = fields[2].Replace("'", "");
                string contactTitle = fields[3].Replace("'", "");
                string address = fields[4].Replace("'", "");
                string city = fields[5].Replace("'", "");
                string region = fields[6].Replace("'", "");
                string postalCode = fields[7].Replace("'", "");
                string country = fields[8].Replace("'", "");
                string phoneNumber = fields[9].Replace("'", "");
                string mobileNumber = fields[10].Replace("'", "");

                if (companyName == "NULL") companyName = "";
                if (contactName == "NULL") contactName = "";
                if (contactTitle == "NULL") contactTitle = "";
                if (address == "NULL") address = "";
                if (city == "NULL") city = "";
                if (region == "NULL") region = "";
                if (postalCode == "NULL") postalCode = "";
                if (country == "NULL") country = "";
                if (phoneNumber == "NULL") phoneNumber = "";
                if (mobileNumber == "NULL") mobileNumber = "";

                int testInt;
                if (int.TryParse(city, out testInt))
                {
                    city = region;
                    region = "";
                }

                Person person = new Person();
                person.Address = address;
                person.City = city;
                person.Region = region;
                person.PostalCode = postalCode;
                person.PhoneNumber = phoneNumber;
                person.MobileNumber = mobileNumber;
                person.CompanyName = companyName;
                person.ContactName = contactName;
                person.ContactTitle = contactTitle;
                person.Country = country;

                persons.Add(person);

            }

            foreach (Person person in persons)
            {
                dbConnection.Persons.Add(person);
            }

        }


    }

}

