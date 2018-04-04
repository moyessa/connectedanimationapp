using System;
using System.Collections.Generic;

namespace ConnectedAnimationApp
{
    public class CustomDataObject
    {
        public String Title { get; set; }
        public String VisitDates { get; set; }
        public String PopularFor { get; set; }
        public String Description { get; set; }

        private String _imageLocation;
        public String ImageLocation
        {
            get => _imageLocation;
            set => _imageLocation = "./Assets/Images/" + value;
        }

        public CustomDataObject()
        {
            Description = "Lorem ipsum dolor";
            ImageLocation = "1.jpg";
        }

        public static List<CustomDataObject> GetDataObjects()
        {
            const int numberOfLocations = 8;

            List<CustomDataObject> objects = new List<CustomDataObject>();

            for (int i = 0; i < numberOfLocations; i++)
            {
                CustomDataObject obj = new CustomDataObject();
                switch (i % numberOfLocations)
                {
                    case 0:
                        obj.Title = "Seattle, Washington";
                        obj.ImageLocation = "seattle.jpg";
                        obj.VisitDates = "Jul-Aug";
                        obj.PopularFor = "Leisure, Outdoors, Entertainment, Museums";
                        obj.Description = "Seattle is a seaport city on the west coast of the United States. It is the seat of King County, Washington. With an estimated 704,352 residents as of 2016, Seattle is the largest city in both the state of Washington and the Pacific Northwest region of North America. In July 2013, it was the fastest-growing major city in the United States and remained in the Top 5 in May 2015 with an annual growth rate of 2.1%. In July 2016, Seattle was again the fastest-growing major U.S. city, with a 3.1% annual growth rate. The city is situated on an isthmus between Puget Sound (an inlet of the Pacific Ocean) and Lake Washington, about 100 miles (160 km) south of the Canada–United States border. A major gateway for trade with Asia, Seattle is the fourth-largest port in North America in terms of container handling as of 2015.";
                        break;
                    case 1:
                        obj.Title = "Denver, Colorado";
                        obj.ImageLocation = "colorado.jpg";
                        obj.VisitDates = "Jul-Aug";
                        obj.PopularFor = "Outdoors, Entertainment, Museums, Leisure";
                        obj.Description = "Denver is the capital and most populous municipality of the U.S. state of Colorado. Denver is in the South Platte River Valley on the western edge of the High Plains just east of the Front Range of the Rocky Mountains. The Denver downtown district is immediately east of the confluence of Cherry Creek with the South Platte River, approximately 12 mi (19 km) east of the foothills of the Rocky Mountains. Denver is nicknamed the Mile-High City because its official elevation is exactly one mile (5280 feet or 1609.3 meters) above sea level, making it the highest major city in the United States.";
                        break;
                    case 2:
                        obj.Title = "Madrid, Spain";
                        obj.ImageLocation = "madrid.jpg";
                        obj.VisitDates = "Jun-Sep";
                        obj.PopularFor = "Arts & Culture, Historical, Leisure, Outdoors";
                        obj.Description = "Madrid is the capital of Spain and the largest municipality in both the Community of Madrid and Spain as a whole. The city has almost 3.166 million inhabitants with a metropolitan area population of approximately 6.5 million. It is the third-largest city in the European Union after London and Berlin, and its metropolitan area is the third-largest in the EU after those of London and Paris. The municipality itself covers an area of 604.3 km².";
                        break;
                    case 3:
                        obj.Title = "Inverness, Scottland";
                        obj.ImageLocation = "inverness.jpg";
                        obj.VisitDates = "Jun-Aug";
                        obj.PopularFor = "Historical, Leisure, Entertainment, Outdoors";
                        obj.Description = "Inverness is a city in the Scottish Highlands. It is the administrative centre for the Highland council area, and is regarded as the capital of the Highlands. Inverness lies near two important battle sites: the 11th-century battle of Blàr nam Fèinne against Norway which took place on The Aird and the 18th-century Battle of Culloden which took place on Culloden Moor. It is the northernmost city in the United Kingdom and lies within the Great Glen at its north-eastern extremity where the River Ness enters the Moray Firth. At the latest, a settlement was established by the 6th century with the first royal charter being granted by Dabíd mac Maíl Choluim in the 12th century. The Gaelic king Mac Bethad Mac Findláich whose 11th-century murder of King Duncan was immortalised in Shakespeare's play Macbeth, held a castle within the city where he ruled as Mormaer of Moray and Ross.";
                        break;
                    case 4:
                        obj.Title = "Maui, Hawaii";
                        obj.ImageLocation = "maui.jpg";
                        obj.VisitDates = "Jun-Aug";
                        obj.PopularFor = "Leisure, Outdoors, Entertainment";
                        obj.Description = "The island of Maui is the second-largest of the Hawaiian Islands at 727.2 square miles and is the 17th-largest island in the United States. Maui is part of the State of Hawaii and is the largest of Maui County's four islands, which include Molokaʻi, Lānaʻi, and unpopulated Kahoʻolawe. In 2010, Maui had a population of 144,444, third-highest of the Hawaiian Islands, behind that of Oʻahu and Hawaiʻi Island. Kahului is the largest census-designated place on the island with a population of 26,337 as of 2010 and is the commercial and financial hub of the island. Wailuku is the seat of Maui County and is the third-largest CDP as of 2010. Other significant places include Kīhei, Lahaina, Makawao, Pukalani, Pāʻia, Kula, Haʻikū, and Hāna.";
                        break;
                    case 5:
                        obj.Title = "Rome, Italy";
                        obj.ImageLocation = "rome.jpg";
                        obj.VisitDates = "May-Sep";
                        obj.PopularFor = "Family & Kids, Arts & Culture, Historical, Leisure";
                        obj.Description = "Rome is the capital of Italy and a special comune. Rome also serves as the capital of the Lazio region. With 2,877,215 residents in 1,285 km², it is also the country's most populated comune. It is the fourth-most populous city in the European Union by population within city limits. It is the center of the Metropolitan City of Rome, which has a population of 4.3 million residents. Rome is located in the central-western portion of the Italian Peninsula, within Lazio, along the shores of the Tiber. The Vatican City is an independent country inside the city boundaries of Rome, the only existing example of a country within a city: for this reason Rome has been often defined as capital of two states.";
                        break;
                    case 6:
                        obj.Title = "San Francisco, California";
                        obj.ImageLocation = "sanfrancisco.jpg";
                        obj.VisitDates = "Sep-Nov";
                        obj.PopularFor = "Arts & Culture, Entertainment, Leisure, Outdoors";
                        obj.Description = "San Francisco, officially the City and County of San Francisco, is the cultural, commercial, and financial center of Northern California. The consolidated city-county covers an area of about 47.9 square miles at the north end of the San Francisco Peninsula in the San Francisco Bay Area. It is the fourth-most populous city in California, and the 13th-most populous in the United States, with a 2016 census-estimated population of 870,887. The population is projected to reach 1 million by 2033.";
                        break;
                    default:
                        obj.Title = "Zermatt, Switzerland";
                        obj.ImageLocation = "zermatt.jpg";
                        obj.VisitDates = "Jun-Aug";
                        obj.PopularFor = "Historical, Leisure, Outdoors";
                        obj.Description = "Zermatt is a statistic town and a municipality in the district of Visp in the German-speaking section of the canton of Valais in Switzerland. It has a population of about 5,800 inhabitants. The town lies at the upper end of Mattertal at an elevation of 1,620 m, at the foot of Switzerland's highest peaks. It lies about 10 km from the over 10,800 ft high Theodul Pass bordering Italy.";
                        break;

                }

                objects.Add(obj);

            }

            return objects;
        }

    }
}
