﻿using PharmacyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShop.ViewModels
{
    public class MedicineConfiguration : Medicine
    {
        private List<Medicine> medicineList = new List<Medicine>();
        //public async Task<List<Medicine>> Medicines()
        //{
        //    var Alvedon = new Medicine
        //    {
        //        Name = "Alvedon",
        //        Dose = "500mg",
        //        Substance = "Paracetamol",
        //        Description = "Brustablett",
        //        Price = 39.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/156341/large/0.jpg",
        //        Useage = "Alvedon 500 mg brustabletter innehåller paracetamol som är smärtlindrande och febernedsättande. Den aktiva substansen och den smärtlindrande effekten upplevs efter cirka 30 minuter samt minskar feber upp till 6-8 timmar. Alvedon används vid feber, t.ex. vid förkylningar. Kan även användas mot värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Alvedon brustabletter har en frisk smak av citrus och löses enkelt upp i ett halvt glas vatten. Brustabletter är ett bra alternativ för personer som har svårt att svälja tabletter och kan användas av personer med känslig mage eller personer med ökad blödningsbenägenhet. Från 12 år. Rekommenderas inte till personer som ordinerats saltfattig kost.Läs alltid bipacksedeln noga före användning."
        //    };
        //    medicineList.Add(Alvedon);

        //    var Alvedon2 = new Medicine
        //    {
        //        Name = "Alvedon",
        //        Dose = "500mg",
        //        Substance = "Paracetamol",
        //        Description = "Filmdragerad",
        //        Price = 32.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/097610/large/0.jpg",
        //        Useage = "Alvedon 500 mg filmdragerad tablett innehåller paracetamol som är smärtlindrande och febernedsättande. Att tabletten är filmdragerad gör att den blir lättare att svälja. Den aktiva substansen börjar verka efter ungefär 30 minuter och lindrar smärtan i upp till 4-5 timmar. Alvedon lindrar feber, t.ex. vid förkylningar. Kan även användas för lindring av värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Läs alltid bipacksedeln noga före användning."
        //    };
        //    medicineList.Add(Alvedon2);

        //    var Alvedon3 = new Medicine
        //    {
        //        Name = "Alvedon",
        //        Dose = "250mg",
        //        Substance = "Paracetamol",
        //        Description = "Brustablett",
        //        Price = 42.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/007487/large/0.jpg",
        //        Useage = "Alvedon 250 mg brustabletter innehåller paracetamol som är smärtlindrande och febernedsättande. Den aktiva substansen och den smärtlindrande effekten upplevs efter cirka 30 minuter samt minskar feber upp till 6-8 timmar. Alvedon används vid feber, t.ex. vid förkylningar. Kan även användas mot värk och smärta av olika slag: t.ex. huvudvärk, tandvärk, menstruationssmärtor och muskelvärk. Alvedon brustabletter har en frisk smak av citrus och löses enkelt upp i ett halvt glas vatten. Brustabletter är ett bra alternativ för personer som har svårt att svälja tabletter och kan användas av personer med känslig mage eller personer med ökad blödningsbenägenhet. Från 12 år. Rekommenderas inte till personer som ordinerats saltfattig kost.Läs alltid bipacksedeln noga före användning."
        //    };
        //    medicineList.Add(Alvedon3);

        //    var Alvedon4 = new Medicine
        //    {
        //        Name = "Alvedon",
        //        Dose = "24 mg/ml",
        //        Substance = "Paracetamol",
        //        Description = "Flytande",
        //        Price = 52.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/126168/large/0.jpg",
        //        Useage = "Alvedon 24 mg/ml oral suspension, nu med en förbättrad formulering. Den nya versionen av Alvedon oral suspension har en behaglig, mjuk konsistens med en mild jordgubbssmak. Det är inte nödvändigt att skaka flaskan före användning. Flytande Alvedon är ett utmärkt val för barn som har svårt att svälja tabletter eller inte kan använda stolpiller. Dessutom är det lätt att dosera med den medföljande doseringssprutan.\r\n\r\nAlvedon hjälper till att lindra symtom vid feber samt värk och smärta av olika slag, såsom huvudvärk, tandvärk och muskelvärk. Den aktiva substansen paracetamol är både smärtlindrande och febernedsättande. Produkten är särskilt anpassad för barn från 3 månader till cirka 12 år (5-40 kg).\r\n\r\nEffektivt mot feber i upp till 8 timmar och mot värk i 4-5 timmar.\r\nSnabb verkan, börjar minska febern redan efter 15 minuter.\r\nEnkel att dosera med medföljande doseringsspruta\r\nMild jordgubbssmak\r\nFör barn som väger mellan 5-40 kg (ca. 3 mån - 12 år)"
        //    };
        //    medicineList.Add(Alvedon4);


        //    var Ipren = new Medicine
        //    {
        //        Name = "Ipren",
        //        Dose = "400mg",
        //        Substance = "Ibuprofen",
        //        Description = "Filmdragerad",
        //        Price = 66.14M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/088542/large/0.jpg",
        //        Useage = "Används vid tillfälliga lätta till måttliga smärttillstånd, t ex huvudvärk, muskel- och ledvärk samt feber vid förkylning."
        //    };
        //    medicineList.Add(Ipren);

        //    var Bufren2 = new Medicine
        //    {
        //        Name = "Ipren",
        //        Dose = "200mg",
        //        Substance = "Ibuprofen",
        //        Description = "Filmdragerad",
        //        Price = 58.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/460949/large/0.jpg",
        //        Useage = "Används vid tillfälliga lätta till måttliga smärttillstånd, t ex huvudvärk, muskel- och ledvärk samt feber vid förkylning."
        //    };
        //    medicineList.Add(Bufren2);

        //    var Panodil = new Medicine
        //    {
        //        Name = "Panodil",
        //        Dose = "400mg",
        //        Substance = "Paracetamol",
        //        Description = "Filmdragerad",
        //        Price = 45.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/196456/large/0.jpg",
        //        Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk.\r\n\r\n\r\n\r\nPanodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
        //    };
        //    medicineList.Add(Panodil);

        //    var Panodil2 = new Medicine
        //    {
        //        Name = "Panodil",
        //        Dose = "24 mg/ml",
        //        Substance = "Paracetamol",
        //        Description = "Oral lösning",
        //        Price = 49.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/014498/large/0.jpg",
        //        Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk.\r\n\r\n\r\n\r\nPanodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
        //    };
        //    medicineList.Add(Panodil2);

        //    var Panodil3 = new Medicine
        //    {
        //        Name = "Panodil",
        //        Dose = "500mg",
        //        Substance = "Paracetamol",
        //        Description = "Brustablett",
        //        Price = 49.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/165273/large/0.jpg",
        //        Useage = "Panodil Zapp används för behandling av tillfälliga feber- och smärttillstånd av lindrig art, t ex feber vid förkylning, huvudvärk, tandvärk, menstruationssmärtor, muskel- och ledvärk.\r\n\r\n\r\n\r\nPanodil Zapp kan användas av personer med känslig mage eller magsår och personer med ökad blödningsbenägenhet. Du måste tala med läkare om symtomen försämras eller inte förbättras inom 3 dagar vid feber och 5 dagar vid smärta."
        //    };
        //    medicineList.Add(Panodil3);

        //    var Naxopren = new Medicine
        //    {
        //        Name = "Naxopren",
        //        Dose = "250mg",
        //        Substance = "Laktosmonhydrat & natrium",
        //        Description = "Oral tablett",
        //        Price = 49.90M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/507504/large/0.jpg",
        //        Useage = "Inflammationsdämpande och smärtstillande vid tillfällig till måttlig akut smärta, t.ex. huvudvärk, tandvärk, muskel- och ledvärk, ryggvärk samt vid menstruationssmärtor hos vuxna och ungdomar från 12 år över 50 kg"
        //    };
        //    medicineList.Add(Naxopren);

        //    var Sedix = new Medicine
        //    {
        //        Name = "Naxopren",
        //        Dose = "200mg",
        //        Substance = "Passionsblomma",
        //        Description = "Dragerad tablett",
        //        Price = 154.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/156375/large/0.jpg",
        //        Useage = "Sedix® är ett traditionellt växtbaserat läkemedel använt vid lindriga symtom på nervös anspänning såsom oro, irritabilitet och tillfälliga insomningsbesvär. Det är inte rapporterat om beroendeframkallande effekt vid användning av Sedix."
        //    };
        //    medicineList.Add(Sedix);

        //    var Melatan = new Medicine
        //    {
        //        Name = "Melatan",
        //        Dose = "3mg",
        //        Substance = "Kalciumvätefosfatdihydrat",
        //        Description = "Tabletter",
        //        Price = 79.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/451044/large/0.jpg",
        //        Useage = "Melatan innehåller den aktiva substansen melatonin, som tillhör en grupp av naturliga hormoner som tillverkas av kroppen. Melatan används vid korttidsbehandling av jetlag hos vuxna från 18 år. Med jetlag avses de symtom såsom t ex sömnbesvär, som orsakas av tidsskillnaden när man passerar flera tidszoner när man reser öster- eller västerut."
        //    };
        //    medicineList.Add(Melatan);

        //    var Ebastin = new Medicine
        //    {
        //        Name = "Ebastin",
        //        Dose = "10mg",
        //        Substance = "Ebastin",
        //        Description = "Filmdragerad",
        //        Price = 95.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/061426/large/0.jpg",
        //        Useage = "Ebastin Orifarm, filmdragerad tablett 10 mg 30 st, är ett receptfritt antihistamin som motverkar effekten av histamin. Histamin är ett ämne i kroppens vävnader som utlöses när man utsätts för allergiframkallande ämnen och som bland annat utlöser rinnsnuva, nästäppa och nysningar samt ögonbesvär såsom röda, rinnande och kliande ögon. Hos känsliga personer kan histamin även orsaka besvärande klåda och hudutslag efter myggbett.\r\n\r\nEbastin Orifarm används vid tillfälliga allergiska besvär såsom röda, rinnande och kliande ögon samt rinnsnuva, nästäppa och nysningar. De allergiska besvären kan orsakas av bland annat pollen, pälsdjur, kvalster eller damm. Ebastin Orifarm kan också användas av personer som är känsliga för myggbett, vid tillfällig vistelse i myggrika områden."
        //    };
        //    medicineList.Add(Ebastin);

        //    var Ebastin1 = new Medicine
        //    {
        //        Name = "Ebastin",
        //        Dose = "10mg",
        //        Substance = "Ebastin",
        //        Description = "Brustablett",
        //        Price = 95.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/588341/large/0.jpg",
        //        Useage = "Ebastin Orifarm, filmdragerad tablett 10 mg 30 st, är ett receptfritt antihistamin som motverkar effekten av histamin. Histamin är ett ämne i kroppens vävnader som utlöses när man utsätts för allergiframkallande ämnen och som bland annat utlöser rinnsnuva, nästäppa och nysningar samt ögonbesvär såsom röda, rinnande och kliande ögon. Hos känsliga personer kan histamin även orsaka besvärande klåda och hudutslag efter myggbett.\r\n\r\nEbastin Orifarm används vid tillfälliga allergiska besvär såsom röda, rinnande och kliande ögon samt rinnsnuva, nästäppa och nysningar. De allergiska besvären kan orsakas av bland annat pollen, pälsdjur, kvalster eller damm. Ebastin Orifarm kan också användas av personer som är känsliga för myggbett, vid tillfällig vistelse i myggrika områden."
        //    };
        //    medicineList.Add(Ebastin1);

        //    var Laxoberal = new Medicine
        //    {
        //        Name = "Laxoberal",
        //        Dose = "7,5 mg/ml",
        //        Substance = "Natriumpikosulfat",
        //        Description = "Orala droppar",
        //        Price = 45.00M,
        //        ImageUrl = "https://www.kronansapotek.se/k2/images/592931/large/0.jpg",
        //        Useage = "Laxoberal orala droppar används vid tillfällig förstoppning.Laxoberal ger mjukare avföring och underlättar tarmpassagen. Verkar lokalt i tjocktarmen. Effekten kommer efter 6-12 timmar.Kan användas från 12 års ålder. Individuell dosering. Dropparna gör det lätt att anpassa dosen efter individuellt behov. Kan blandas med mat eller dryck.Laxoberal är ett receptfritt läkemedel, som innehåller det aktiva ämnet natriumpikosulfat. Läs bipacksedeln noga före användning."
        //    };
        //    medicineList.Add(Laxoberal);

           

        //    return medicineList;
        //}
    }
}
