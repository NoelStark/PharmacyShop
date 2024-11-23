using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.Services
{
	public class MedicineService
	{
		private List<Medicine> medicineList = new List<Medicine>();
		public Medicine CurrentMedicine {  get; set; } = new Medicine();

		public List<Medicine> Medicines()
		{

				var Alvedon = new Medicine
				{
					Name = "Alvedon",
					Dose = "500mg",
					Substance = "Paracetamol",
					Description = "Brustablett",
					ArticleNumber = 10234,
					Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/156341/large/0.jpg",
						ItemPrice = 39.90M
					},
					Amount = "20pc",
					Useage = "Alvedon 500 mg brustabletter innehåller paracetamol som är smärtlindrande och febernedsättande. Den aktiva substansen och den smärtlindrande effekten upplevs efter cirka 30 minuter samt minskar feber upp till 6-8 timmar. Alvedon används vid feber, t.ex. vid förkylningar. Kan även användas mot värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Alvedon brustabletter har en frisk smak av citrus och löses enkelt upp i ett halvt glas vatten. Brustabletter är ett bra alternativ för personer som har svårt att svälja tabletter och kan användas av personer med känslig mage eller personer med ökad blödningsbenägenhet. Från 12 år. Rekommenderas inte till personer som ordinerats saltfattig kost. Läs alltid bipacksedeln noga före användning."
				};
				medicineList.Add(Alvedon);

				var Alvedon2 = new Medicine
				{
					Name = "Alvedon",
					Dose = "500mg",
					Substance = "Paracetamol",
					Description = "Filmdragerad",
                    ArticleNumber = 14567,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/097610/large/0.jpg",
						ItemPrice = 32.00M
					},
					Amount = "20pc",
					Useage = "Alvedon 500 mg filmdragerad tablett innehåller paracetamol som är smärtlindrande och febernedsättande. Att tabletten är filmdragerad gör att den blir lättare att svälja. Den aktiva substansen börjar verka efter ungefär 30 minuter och lindrar smärtan i upp till 4-5 timmar. Alvedon lindrar feber, t.ex. vid förkylningar. Kan även användas för lindring av värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Läs alltid bipacksedeln noga före användning."
				};
				medicineList.Add(Alvedon2);

				var Alvedon3 = new Medicine
				{
					Name = "Alvedon",
					Dose = "250mg",
					Substance = "Paracetamol",
					Description = "Brustablett",
                    ArticleNumber = 18321,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/007487/large/0.jpg",
						ItemPrice = 42.90M
					},
					Amount = "12pc",
					Useage = "Alvedon 250 mg brustabletter innehåller paracetamol som är smärtlindrande och febernedsättande. Den aktiva substansen och den smärtlindrande effekten upplevs efter cirka 30 minuter samt minskar feber upp till 6-8 timmar. Alvedon används vid feber, t.ex. vid förkylningar. Kan även användas mot värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Alvedon brustabletter har en frisk smak av citrus och löses enkelt upp i ett halvt glas vatten. Brustabletter är ett bra alternativ för personer som har svårt att svälja tabletter och kan användas av personer med känslig mage eller personer med ökad blödningsbenägenhet. Från 12 år. Rekommenderas inte till personer som ordinerats saltfattig kost. Läs alltid bipacksedeln noga före användning."
				};
				medicineList.Add(Alvedon3);

				var Alvedon4 = new Medicine
				{
					Name = "Alvedon",
					Dose = "24 mg/ml",
					Substance = "Paracetamol",
					Description = "Flytande",
                    ArticleNumber = 19203,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/126168/large/0.jpg",
						ItemPrice = 52.00M
					},
					Amount = "100ml",
					Useage = "Alvedon 24 mg/ml oral suspension, nu med en förbättrad formulering. Den nya versionen av Alvedon oral suspension har en behaglig, mjuk konsistens med en mild jordgubbssmak. Det är inte nödvändigt att skaka flaskan före användning. Flytande Alvedon är ett utmärkt val för barn som har svårt att svälja tabletter eller inte kan använda stolpiller. Dessutom är det lätt att dosera med den medföljande doseringssprutan. Alvedon hjälper till att lindra symtom vid feber samt värk och smärta av olika slag, såsom huvudvärk, tandvärk och muskelvärk. Den aktiva substansen paracetamol är både smärtlindrande och febernedsättande. Produkten är särskilt anpassad för barn från 3 månader till cirka 12 år (5-40 kg). Effektivt mot feber i upp till 8 timmar och mot värk i 4-5 timmar. Snabb verkan, börjar minska febern redan efter 15 minuter. Enkel att dosera med medföljande doseringsspruta. Mild jordgubbssmak. För barn som väger mellan 5-40 kg (ca. 3 mån - 12 år)"
				};
				medicineList.Add(Alvedon4);

				var Ipren = new Medicine
				{
					Name = "Ipren",
					Dose = "400mg",
					Substance = "Ibuprofen",
					Description = "Filmdragerad",
                    ArticleNumber = 20456,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/088542/large/0.jpg",
						ItemPrice = 66.14M
					},
					Amount = "10pc",
					Useage = "Används vid tillfälliga lätta till måttliga smärttillstånd, t ex huvudvärk, muskel- och ledvärk samt feber vid förkylning."
				};
				medicineList.Add(Ipren);

				var Bufren2 = new Medicine
				{
					Name = "Ipren",
					Dose = "200mg",
					Substance = "Ibuprofen",
					Description = "Filmdragerad",
                    ArticleNumber = 21345,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/460949/large/0.jpg",
						ItemPrice = 58.90M
					},
					Amount = "30pc",
					Useage = "Används vid tillfälliga lätta till måttliga smärttillstånd, t ex huvudvärk, muskel- och ledvärk samt feber vid förkylning."
				};
				medicineList.Add(Bufren2);

				var Panodil = new Medicine
				{
					Name = "Panodil",
					Dose = "400mg",
					Substance = "Paracetamol",
					Description = "Filmdragerad",
                    ArticleNumber = 23451,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/196456/large/0.jpg",
						ItemPrice = 45.90M
					},
					Amount = "20pc",
					Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk. Panodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
				};
				medicineList.Add(Panodil);

				var Panodil2 = new Medicine
				{
					Name = "Panodil",
					Dose = "24 mg/ml",
					Substance = "Paracetamol",
					Description = "Oral lösning",
                    ArticleNumber = 24567,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/014498/large/0.jpg",
						ItemPrice = 49.00M
					},
					Amount = "100ml",
					Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk. Panodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
				};
				medicineList.Add(Panodil2);

				var Panodil3 = new Medicine
				{
					Name = "Panodil",
					Dose = "500mg",
					Substance = "Paracetamol",
					Description = "Brustablett",
                    ArticleNumber = 25678,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/165273/large/0.jpg",
						ItemPrice = 49.90M
					},
					Amount = "20pc",
					Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk. Panodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
				};
				medicineList.Add(Panodil3);

				var Naxopren = new Medicine
				{
					Name = "Naxopren",
					Dose = "250mg",
					Substance = "Laktosmonhydrat & natrium",
					Description = "Oral tablett",
                    ArticleNumber = 27345,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/507504/large/0.jpg",
						ItemPrice = 49.90M
					},
					Amount = "20pc",
					Useage = "Inflammationsdämpande och smärtstillande vid tillfällig till måttlig akut smärta, t.ex. huvudvärk, tandvärk, muskel- och ledvärk, ryggvärk samt vid menstruationssmärtor hos vuxna och ungdomar från 12 år över 50 kg."
				};
				medicineList.Add(Naxopren);

				var Sedix = new Medicine
				{
					Name = "Naxopren",
					Dose = "200mg",
					Substance = "Passionsblomma",
					Description = "Dragerad tablett",
                    ArticleNumber = 28912,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/156375/large/0.jpg",
						ItemPrice = 154.00M
					},
					Amount = "28pc",
					Useage = "Sedix® är ett traditionellt växtbaserat läkemedel använt vid lindriga symtom på nervös anspänning såsom oro, irritabilitet och tillfälliga insomningsbesvär. Det är inte rapporterat om beroendeframkallande effekt vid användning av Sedix."
				};
				medicineList.Add(Sedix);

				var Melatan = new Medicine
				{
					Name = "Melatan",
					Dose = "3mg",
					Substance = "Kalciumvätefosfatdihydrat",
					Description = "Tabletter",
                    ArticleNumber = 29876,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/451044/large/0.jpg",
						ItemPrice = 79.00M
					},
					Amount = "10pc",
					Useage = "Melatan innehåller den aktiva substansen melatonin, som tillhör en grupp av naturliga hormoner som tillverkas av kroppen. Melatan används vid korttidsbehandling av jetlag hos vuxna från 18 år. Med jetlag avses de symtom såsom t ex sömnbesvär, som orsakas av tidsskillnaden när man passerar flera tidszoner när man reser öster- eller västerut."
				};
				medicineList.Add(Melatan);

				var Ebastin = new Medicine
				{
					Name = "Ebastin",
					Dose = "10mg",
					Substance = "Ebastin",
					Description = "Filmdragerad",
                    ArticleNumber = 30123,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/061426/large/0.jpg",
						ItemPrice = 95.00M
					},
					Amount = "30pc",
					Useage = "Ebastin Orifarm, filmdragerad tablett 10 mg 30 st, är ett receptfritt antihistamin som motverkar effekten av histamin. Histamin är ett ämne i kroppens vävnader som utlöses när man utsätts för allergiframkallande ämnen och som bland annat utlöser rinnsnuva, nästäppa och nysningar samt ögonbesvär såsom röda, rinnande och kliande ögon. Hos känsliga personer kan histamin även orsaka besvärande klåda och hudutslag efter myggbett. Ebastin Orifarm används vid tillfälliga allergiska besvär såsom röda, rinnande och kliande ögon samt rinnsnuva, nästäppa och nysningar. De allergiska besvären kan orsakas av bland annat pollen, pälsdjur, kvalster eller damm. Ebastin Orifarm kan också användas av personer som är känsliga för myggbett, vid tillfällig vistelse i myggrika områden."
				};
				medicineList.Add(Ebastin);

				var Ebastin1 = new Medicine
				{
					Name = "Ebastin",
					Dose = "10mg",
					Substance = "Ebastin",
					Description = "Brustablett",
                    ArticleNumber = 31234,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/588341/large/0.jpg",
						ItemPrice = 95.00M
					},
					Amount = "30pc",
					Useage = "Ebastin Orifarm, filmdragerad tablett 10 mg 30 st, är ett receptfritt antihistamin som motverkar effekten av histamin. Histamin är ett ämne i kroppens vävnader som utlöses när man utsätts för allergiframkallande ämnen och som bland annat utlöser rinnsnuva, nästäppa och nysningar samt ögonbesvär såsom röda, rinnande och kliande ögon. Hos känsliga personer kan histamin även orsaka besvärande klåda och hudutslag efter myggbett. Ebastin Orifarm används vid tillfälliga allergiska besvär såsom röda, rinnande och kliande ögon samt rinnsnuva, nästäppa och nysningar. De allergiska besvären kan orsakas av bland annat pollen, pälsdjur, kvalster eller damm. Ebastin Orifarm kan också användas av personer som är känsliga för myggbett, vid tillfällig vistelse i myggrika områden."
				};
				medicineList.Add(Ebastin1);

				var Laxoberal = new Medicine
				{
					Name = "Laxoberal",
					Dose = "7,5 mg/ml",
					Substance = "Natriumpikosulfat",
					Description = "Orala droppar",
                    ArticleNumber = 34567,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/592931/large/0.jpg",
						ItemPrice = 45.00M
					},
					Amount = "30ml",
					Useage = "Laxoberal orala droppar används vid tillfällig förstoppning. Laxoberal ger mjukare avföring och underlättar tarmpassagen. Verkar lokalt i tjocktarmen. Effekten kommer efter 6-12 timmar. Kan användas från 12 års ålder. Individuell dosering. Dropparna gör det lätt att anpassa dosen efter individuellt behov. Kan blandas med mat eller dryck. Laxoberal är ett receptfritt läkemedel, som innehåller det aktiva ämnet natriumpikosulfat. Läs bipacksedeln noga före användning."
				};
				medicineList.Add(Laxoberal);

				var Strepsils = new Medicine
				{
					Name = "Strepsils",
					Dose = "2,4 mg",
					Substance = "Diklorbenzylalkohol",
					Description = "Sugtablett",
                    ArticleNumber = 35678,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/127383/large/0.jpg",
						ItemPrice = 86.00M
					},
					Amount = "36pc",
					Useage = "Virushämmande & antibakteriell. Smärtlindring på 5 min. Används vid infektioner i munhåla & svalg. Effekt i 2 timmar.Innehåller honung. Från 6 år. Strepsils Honung & Citron är en halstablett som används vid lindriga infektioner i munhåla och svalg, till exempel vid ont i halsen. Strepsils Honung & Citron har en bakteriehämmande och antiviral effekt."
				};
				medicineList.Add(Strepsils);


				var Alsolsprit = new Medicine
				{
					Name = "Alsolsprit",
					Dose = "10 mg/ml",
					Substance = "Aluminiumacetotartrat",
					Description = "Kutan lösning",
                    ArticleNumber = 36789,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/786270/large/0.jpg",
						ItemPrice = 45.00M
					},
					Amount = "250ml",
					Useage = "Alsolsprit kutan lösning är en lindrande lösning mot exempelvis klåda vid insektsbett. Rengörande med mild uttorkande och sammandragande effekt. Går bra att använda vid graviditet och under amning men inte för behandling av bröstvårtor. Färdig att använda, baddas på huden med hjälp av exempelvis en bomullsrondell. Undvik kontakt med ögonen."
				};
				medicineList.Add(Alsolsprit);

				var Fexofenadin = new Medicine
				{
					Name = "Fexofenadin",
					Dose = "120mg",
					Substance = "Fexofenadinhydroklorid",
					Description = "Filmdragerad",
                    ArticleNumber = 37890,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/385632/large/0.jpg",
						ItemPrice = 75.00M
					},
					Amount = "30pc",
					Useage = "Tablett för symtomlindring vid säsongsbunden allergisk rinit (hösnuva). Används hos vuxna och ungdomar från 12 års ålder för att lindra symtom som förekommer vid hösnuva (säsongsbunden allergisk rinit) såsom nysningar, kliande, rinnande eller täppt näsa och kliande, röda och vattniga ögon."
				};
				medicineList.Add(Fexofenadin);

				var Bisolvon = new Medicine
				{
					Name = "Bisolvon",
					Dose = "8mg",
					Substance = "Bromhexinhydroklorid",
					Description = "Tabletter",
                    ArticleNumber = 39012,
                    Information = new ItemInformation
					{
						ImageUrl = "https://www.kronansapotek.se/k2/images/420984/large/0.jpg",
						ItemPrice = 86.00M
					},
					Amount = "100pc",
					Useage = "Bisolvon anses lösa upp segt slem så att det blir mer tunnflytande och därmed lättare att hosta upp. Bisolvon används vid tillfällig kortvarig hosta med segt slem."
				};
				medicineList.Add(Bisolvon);

				return medicineList;
		}
	}
}
