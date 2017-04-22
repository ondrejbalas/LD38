﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PuddleJumper.Core.Generators
{
    public class AirportNameGenerator
    {
        private ILookup<char, string> cityNamesLookup;
        private Random rng = new Random();

        public AirportNameGenerator()
        {
            cityNamesLookup = CityNames().Where(cn => cn.Length <= 14).ToLookup(c => c[0]);
        }

        public string GetAirportName(char startingCharacter)
        {
            var matchingCities = cityNamesLookup[startingCharacter].ToList();
            return matchingCities[rng.Next(matchingCities.Count)];
        }

        private IEnumerable<string> CityNames()
        {
            yield return "New York";
            yield return "Los Angeles";
            yield return "Chicago";
            yield return "Houston";
            yield return "Philadelphia";
            yield return "Phoenix";
            yield return "San Antonio";
            yield return "San Diego";
            yield return "Dallas";
            yield return "San Jose";
            yield return "Austin";
            yield return "Indianapolis";
            yield return "Jacksonville";
            yield return "San Francisco";
            yield return "Columbus";
            yield return "Charlotte";
            yield return "Fort Worth";
            yield return "Detroit";
            yield return "El Paso";
            yield return "Memphis";
            yield return "Seattle";
            yield return "Denver";
            yield return "Washington";
            yield return "Boston";
            yield return "Nashville-Davidson";
            yield return "Baltimore";
            yield return "Oklahoma City";
            yield return "Louisville/Jefferson County";
            yield return "Portland";
            yield return "Las Vegas";
            yield return "Milwaukee";
            yield return "Albuquerque";
            yield return "Tucson";
            yield return "Fresno";
            yield return "Sacramento";
            yield return "Long Beach";
            yield return "Kansas City";
            yield return "Mesa";
            yield return "Virginia Beach";
            yield return "Atlanta";
            yield return "Colorado Springs";
            yield return "Omaha";
            yield return "Raleigh";
            yield return "Miami";
            yield return "Oakland";
            yield return "Minneapolis";
            yield return "Tulsa";
            yield return "Cleveland";
            yield return "Wichita";
            yield return "Arlington";
            yield return "New Orleans";
            yield return "Bakersfield";
            yield return "Tampa";
            yield return "Honolulu";
            yield return "Aurora";
            yield return "Anaheim";
            yield return "Santa Ana";
            yield return "St. Louis";
            yield return "Riverside";
            yield return "Corpus Christi";
            yield return "Lexington-Fayette";
            yield return "Pittsburgh";
            yield return "Anchorage";
            yield return "Stockton";
            yield return "Cincinnati";
            yield return "St. Paul";
            yield return "Toledo";
            yield return "Greensboro";
            yield return "Newark";
            yield return "Plano";
            yield return "Henderson";
            yield return "Lincoln";
            yield return "Buffalo";
            yield return "Jersey City";
            yield return "Chula Vista";
            yield return "Fort Wayne";
            yield return "Orlando";
            yield return "St. Petersburg";
            yield return "Chandler";
            yield return "Laredo";
            yield return "Norfolk";
            yield return "Durham";
            yield return "Madison";
            yield return "Lubbock";
            yield return "Irvine";
            yield return "Winston-Salem";
            yield return "Glendale";
            yield return "Garland";
            yield return "Hialeah";
            yield return "Reno";
            yield return "Chesapeake";
            yield return "Gilbert";
            yield return "Baton Rouge";
            yield return "Irving";
            yield return "Scottsdale";
            yield return "North Las Vegas";
            yield return "Fremont";
            yield return "Boise City";
            yield return "Richmond";
            yield return "San Bernardino";
            yield return "Birmingham";
            yield return "Spokane";
            yield return "Rochester";
            yield return "Des Moines";
            yield return "Modesto";
            yield return "Fayetteville";
            yield return "Tacoma";
            yield return "Oxnard";
            yield return "Fontana";
            yield return "Columbus";
            yield return "Montgomery";
            yield return "Moreno Valley";
            yield return "Shreveport";
            yield return "Aurora";
            yield return "Yonkers";
            yield return "Akron";
            yield return "Huntington Beach";
            yield return "Little Rock";
            yield return "Augusta-Richmond County";
            yield return "Amarillo";
            yield return "Glendale";
            yield return "Mobile";
            yield return "Grand Rapids";
            yield return "Salt Lake City";
            yield return "Tallahassee";
            yield return "Huntsville";
            yield return "Grand Prairie";
            yield return "Knoxville";
            yield return "Worcester";
            yield return "Newport News";
            yield return "Brownsville";
            yield return "Overland Park";
            yield return "Santa Clarita";
            yield return "Providence";
            yield return "Garden Grove";
            yield return "Chattanooga";
            yield return "Oceanside";
            yield return "Jackson";
            yield return "Fort Lauderdale";
            yield return "Santa Rosa";
            yield return "Rancho Cucamonga";
            yield return "Port St. Lucie";
            yield return "Tempe";
            yield return "Ontario";
            yield return "Vancouver";
            yield return "Cape Coral";
            yield return "Sioux Falls";
            yield return "Springfield";
            yield return "Peoria";
            yield return "Pembroke Pines";
            yield return "Elk Grove";
            yield return "Salem";
            yield return "Lancaster";
            yield return "Corona";
            yield return "Eugene";
            yield return "Palmdale";
            yield return "Salinas";
            yield return "Springfield";
            yield return "Pasadena";
            yield return "Fort Collins";
            yield return "Hayward";
            yield return "Pomona";
            yield return "Cary";
            yield return "Rockford";
            yield return "Alexandria";
            yield return "Escondido";
            yield return "McKinney";
            yield return "Kansas City";
            yield return "Joliet";
            yield return "Sunnyvale";
            yield return "Torrance";
            yield return "Bridgeport";
            yield return "Lakewood";
            yield return "Hollywood";
            yield return "Paterson";
            yield return "Naperville";
            yield return "Syracuse";
            yield return "Mesquite";
            yield return "Dayton";
            yield return "Savannah";
            yield return "Clarksville";
            yield return "Orange";
            yield return "Pasadena";
            yield return "Fullerton";
            yield return "Killeen";
            yield return "Frisco";
            yield return "Hampton";
            yield return "McAllen";
            yield return "Warren";
            yield return "Bellevue";
            yield return "West Valley City";
            yield return "Columbia";
            yield return "Olathe";
            yield return "Sterling Heights";
            yield return "New Haven";
            yield return "Miramar";
            yield return "Waco";
            yield return "Thousand Oaks";
            yield return "Cedar Rapids";
            yield return "Charleston";
            yield return "Visalia";
            yield return "Topeka";
            yield return "Elizabeth";
            yield return "Gainesville";
            yield return "Thornton";
            yield return "Roseville";
            yield return "Carrollton";
            yield return "Coral Springs";
            yield return "Stamford";
            yield return "Simi Valley";
            yield return "Concord";
            yield return "Hartford";
            yield return "Kent";
            yield return "Lafayette";
            yield return "Midland";
            yield return "Surprise";
            yield return "Denton";
            yield return "Victorville";
            yield return "Evansville";
            yield return "Santa Clara";
            yield return "Abilene";
            yield return "Athens-Clarke County";
            yield return "Vallejo";
            yield return "Allentown";
            yield return "Norman";
            yield return "Beaumont";
            yield return "Independence";
            yield return "Murfreesboro";
            yield return "Ann Arbor";
            yield return "Springfield";
            yield return "Berkeley";
            yield return "Peoria";
            yield return "Provo";
            yield return "El Monte";
            yield return "Columbia";
            yield return "Lansing";
            yield return "Fargo";
            yield return "Downey";
            yield return "Costa Mesa";
            yield return "Wilmington";
            yield return "Arvada";
            yield return "Inglewood";
            yield return "Miami Gardens";
            yield return "Carlsbad";
            yield return "Westminster";
            yield return "Rochester";
            yield return "Odessa";
            yield return "Manchester";
            yield return "Elgin";
            yield return "West Jordan";
            yield return "Round Rock";
            yield return "Clearwater";
            yield return "Waterbury";
            yield return "Gresham";
            yield return "Fairfield";
            yield return "Billings";
            yield return "Lowell";
            yield return "San Buenaventura (Ventura)";
            yield return "Pueblo";
            yield return "High Point";
            yield return "West Covina";
            yield return "Richmond";
            yield return "Murrieta";
            yield return "Cambridge";
            yield return "Antioch";
            yield return "Temecula";
            yield return "Norwalk";
            yield return "Centennial";
            yield return "Everett";
            yield return "Palm Bay";
            yield return "Wichita Falls";
            yield return "Green Bay";
            yield return "Daly City";
            yield return "Burbank";
            yield return "Richardson";
            yield return "Pompano Beach";
            yield return "North Charleston";
            yield return "Broken Arrow";
            yield return "Boulder";
            yield return "West Palm Beach";
            yield return "Santa Maria";
            yield return "El Cajon";
            yield return "Davenport";
            yield return "Rialto";
            yield return "Las Cruces";
            yield return "San Mateo";
            yield return "Lewisville";
            yield return "South Bend";
            yield return "Lakeland";
            yield return "Erie";
            yield return "Tyler";
            yield return "Pearland";
            yield return "College Station";
            yield return "Kenosha";
            yield return "Sandy Springs";
            yield return "Clovis";
            yield return "Flint";
            yield return "Roanoke";
            yield return "Albany";
            yield return "Jurupa Valley";
            yield return "Compton";
            yield return "San Angelo";
            yield return "Hillsboro";
            yield return "Lawton";
            yield return "Renton";
            yield return "Vista";
            yield return "Davie";
            yield return "Greeley";
            yield return "Mission Viejo";
            yield return "Portsmouth";
            yield return "Dearborn";
            yield return "South Gate";
            yield return "Tuscaloosa";
            yield return "Livonia";
            yield return "New Bedford";
            yield return "Vacaville";
            yield return "Brockton";
            yield return "Roswell";
            yield return "Beaverton";
            yield return "Quincy";
            yield return "Sparks";
            yield return "Yakima";
            yield return "Lee's Summit";
            yield return "Federal Way";
            yield return "Carson";
            yield return "Santa Monica";
            yield return "Hesperia";
            yield return "Allen";
            yield return "Rio Rancho";
            yield return "Yuma";
            yield return "Westminster";
            yield return "Orem";
            yield return "Lynn";
            yield return "Redding";
            yield return "Spokane Valley";
            yield return "Miami Beach";
            yield return "League City";
            yield return "Lawrence";
            yield return "Santa Barbara";
            yield return "Plantation";
            yield return "Sandy";
            yield return "Sunrise";
            yield return "Macon";
            yield return "Longmont";
            yield return "Boca Raton";
            yield return "San Marcos";
            yield return "Greenville";
            yield return "Waukegan";
            yield return "Fall River";
            yield return "Chico";
            yield return "Newton";
            yield return "San Leandro";
            yield return "Reading";
            yield return "Norwalk";
            yield return "Fort Smith";
            yield return "Newport Beach";
            yield return "Asheville";
            yield return "Nashua";
            yield return "Edmond";
            yield return "Whittier";
            yield return "Nampa";
            yield return "Bloomington";
            yield return "Deltona";
            yield return "Hawthorne";
            yield return "Duluth";
            yield return "Carmel";
            yield return "Suffolk";
            yield return "Clifton";
            yield return "Citrus Heights";
            yield return "Livermore";
            yield return "Tracy";
            yield return "Alhambra";
            yield return "Kirkland";
            yield return "Trenton";
            yield return "Ogden";
            yield return "Hoover";
            yield return "Cicero";
            yield return "Fishers";
            yield return "Sugar Land";
            yield return "Danbury";
            yield return "Meridian";
            yield return "Indio";
            yield return "Concord";
            yield return "Menifee";
            yield return "Champaign";
            yield return "Buena Park";
            yield return "Troy";
            yield return "O'Fallon";
            yield return "Johns Creek";
            yield return "Bellingham";
            yield return "Westland";
            yield return "Bloomington";
            yield return "Sioux City";
            yield return "Warwick";
            yield return "Hemet";
            yield return "Longview";
            yield return "Farmington Hills";
            yield return "Bend";
            yield return "Lakewood";
            yield return "Merced";
            yield return "Mission";
            yield return "Chino";
            yield return "Redwood City";
            yield return "Edinburg";
            yield return "Cranston";
            yield return "Parma";
            yield return "New Rochelle";
            yield return "Lake Forest";
            yield return "Napa";
            yield return "Hammond";
            yield return "Fayetteville";
            yield return "Bloomington";
            yield return "Avondale";
            yield return "Somerville";
            yield return "Palm Coast";
            yield return "Bryan";
            yield return "Gary";
            yield return "Largo";
            yield return "Brooklyn Park";
            yield return "Tustin";
            yield return "Racine";
            yield return "Deerfield Beach";
            yield return "Lynchburg";
            yield return "Mountain View";
            yield return "Medford";
            yield return "Lawrence";
            yield return "Bellflower";
            yield return "Melbourne";
            yield return "St. Joseph";
            yield return "Camden";
            yield return "St. George";
            yield return "Kennewick";
            yield return "Baldwin Park";
            yield return "Chino Hills";
            yield return "Alameda";
            yield return "Albany";
            yield return "Arlington Heights";
            yield return "Scranton";
            yield return "Evanston";
            yield return "Kalamazoo";
            yield return "Baytown";
            yield return "Upland";
            yield return "Springdale";
            yield return "Bethlehem";
            yield return "Schaumburg";
            yield return "Mount Pleasant";
            yield return "Auburn";
            yield return "Decatur";
            yield return "San Ramon";
            yield return "Pleasanton";
            yield return "Wyoming";
            yield return "Lake Charles";
            yield return "Plymouth";
            yield return "Bolingbrook";
            yield return "Pharr";
            yield return "Appleton";
            yield return "Gastonia";
            yield return "Folsom";
            yield return "Southfield";
            yield return "Rochester Hills";
            yield return "New Britain";
            yield return "Goodyear";
            yield return "Canton";
            yield return "Warner Robins";
            yield return "Union City";
            yield return "Perris";
            yield return "Manteca";
            yield return "Iowa City";
            yield return "Jonesboro";
            yield return "Wilmington";
            yield return "Lynwood";
            yield return "Loveland";
            yield return "Pawtucket";
            yield return "Boynton Beach";
            yield return "Waukesha";
            yield return "Gulfport";
            yield return "Apple Valley";
            yield return "Passaic";
            yield return "Rapid City";
            yield return "Layton";
            yield return "Lafayette";
            yield return "Turlock";
            yield return "Muncie";
            yield return "Temple";
            yield return "Missouri City";
            yield return "Redlands";
            yield return "Santa Fe";
            yield return "Lauderhill";
            yield return "Milpitas";
            yield return "Palatine";
            yield return "Missoula";
            yield return "Rock Hill";
            yield return "Jacksonville";
            yield return "Franklin";
            yield return "Flagstaff";
            yield return "Flower Mound";
            yield return "Weston";
            yield return "Waterloo";
            yield return "Union City";
            yield return "Mount Vernon";
            yield return "Fort Myers";
            yield return "Dothan";
            yield return "Rancho Cordova";
            yield return "Redondo Beach";
            yield return "Jackson";
            yield return "Pasco";
            yield return "St. Charles";
            yield return "Eau Claire";
            yield return "North Richland Hills";
            yield return "Bismarck";
            yield return "Yorba Linda";
            yield return "Kenner";
            yield return "Walnut Creek";
            yield return "Frederick";
            yield return "Oshkosh";
            yield return "Pittsburg";
            yield return "Palo Alto";
            yield return "Bossier City";
            yield return "Portland";
            yield return "St. Cloud";
            yield return "Davis";
            yield return "South San Francisco";
            yield return "Camarillo";
            yield return "North Little Rock";
            yield return "Schenectady";
            yield return "Gaithersburg";
            yield return "Harlingen";
            yield return "Woodbury";
            yield return "Eagan";
            yield return "Yuba City";
            yield return "Maple Grove";
            yield return "Youngstown";
            yield return "Skokie";
            yield return "Kissimmee";
            yield return "Johnson City";
            yield return "Victoria";
            yield return "San Clemente";
            yield return "Bayonne";
            yield return "Laguna Niguel";
            yield return "East Orange";
            yield return "Shawnee";
            yield return "Homestead";
            yield return "Rockville";
            yield return "Delray Beach";
            yield return "Janesville";
            yield return "Conway";
            yield return "Pico Rivera";
            yield return "Lorain";
            yield return "Montebello";
            yield return "Lodi";
            yield return "New Braunfels";
            yield return "Marysville";
            yield return "Tamarac";
            yield return "Madera";
            yield return "Conroe";
            yield return "Santa Cruz";
            yield return "Eden Prairie";
            yield return "Cheyenne";
            yield return "Daytona Beach";
            yield return "Alpharetta";
            yield return "Hamilton";
            yield return "Waltham";
            yield return "Coon Rapids";
            yield return "Haverhill";
            yield return "Council Bluffs";
            yield return "Taylor";
            yield return "Utica";
            yield return "Ames";
            yield return "La Habra";
            yield return "Encinitas";
            yield return "Bowling Green";
            yield return "Burnsville";
            yield return "Greenville";
            yield return "West Des Moines";
            yield return "Cedar Park";
            yield return "Tulare";
            yield return "Monterey Park";
            yield return "Vineland";
            yield return "Terre Haute";
            yield return "North Miami";
            yield return "Mansfield";
            yield return "West Allis";
            yield return "Bristol";
            yield return "Taylorsville";
            yield return "Malden";
            yield return "Meriden";
            yield return "Blaine";
            yield return "Wellington";
            yield return "Cupertino";
            yield return "Springfield";
            yield return "Rogers";
            yield return "St. Clair Shores";
            yield return "Gardena";
            yield return "Pontiac";
            yield return "National City";
            yield return "Grand Junction";
            yield return "Rocklin";
            yield return "Chapel Hill";
            yield return "Casper";
            yield return "Broomfield";
            yield return "Petaluma";
            yield return "South Jordan";
            yield return "Springfield";
            yield return "Great Falls";
            yield return "Lancaster";
            yield return "North Port";
            yield return "Lakewood";
            yield return "Marietta";
            yield return "San Rafael";
            yield return "Royal Oak";
            yield return "Des Plaines";
            yield return "Huntington Park";
            yield return "La Mesa";
            yield return "Orland Park";
            yield return "Auburn";
            yield return "Lakeville";
            yield return "Owensboro";
            yield return "Moore";
            yield return "Jupiter";
            yield return "Idaho Falls";
            yield return "Dubuque";
            yield return "Bartlett";
            yield return "Rowlett";
            yield return "Novi";
            yield return "White Plains";
            yield return "Arcadia";
            yield return "Redmond";
            yield return "Lake Elsinore";
            yield return "Ocala";
            yield return "Tinley Park";
            yield return "Port Orange";
            yield return "Medford";
            yield return "Oak Lawn";
            yield return "Rocky Mount";
            yield return "Kokomo";
            yield return "Coconut Creek";
            yield return "Bowie";
            yield return "Berwyn";
            yield return "Midwest City";
            yield return "Fountain Valley";
            yield return "Buckeye";
            yield return "Dearborn Heights";
            yield return "Woodland";
            yield return "Noblesville";
            yield return "Valdosta";
            yield return "Diamond Bar";
            yield return "Manhattan";
            yield return "Santee";
            yield return "Taunton";
            yield return "Sanford";
            yield return "Kettering";
            yield return "New Brunswick";
            yield return "Decatur";
            yield return "Chicopee";
            yield return "Anderson";
            yield return "Margate";
            yield return "Weymouth Town";
            yield return "Hempstead";
            yield return "Corvallis";
            yield return "Eastvale";
            yield return "Porterville";
            yield return "West Haven";
            yield return "Brentwood";
            yield return "Paramount";
            yield return "Grand Forks";
            yield return "Georgetown";
            yield return "St. Peters";
            yield return "Shoreline";
            yield return "Mount Prospect";
            yield return "Hanford";
            yield return "Normal";
            yield return "Rosemead";
            yield return "Lehi";
            yield return "Pocatello";
            yield return "Highland";
            yield return "Novato";
            yield return "Port Arthur";
            yield return "Carson City";
            yield return "San Marcos";
            yield return "Hendersonville";
            yield return "Elyria";
            yield return "Revere";
            yield return "Pflugerville";
            yield return "Greenwood";
            yield return "Bellevue";
            yield return "Wheaton";
            yield return "Smyrna";
            yield return "Sarasota";
            yield return "Blue Springs";
            yield return "Colton";
            yield return "Euless";
            yield return "Castle Rock";
            yield return "Cathedral City";
            yield return "Kingsport";
            yield return "Lake Havasu City";
            yield return "Pensacola";
            yield return "Hoboken";
            yield return "Yucaipa";
            yield return "Watsonville";
            yield return "Richland";
            yield return "Delano";
            yield return "Hoffman Estates";
            yield return "Florissant";
            yield return "Placentia";
            yield return "West New York";
            yield return "Dublin";
            yield return "Oak Park";
            yield return "Peabody";
            yield return "Perth Amboy";
            yield return "Battle Creek";
            yield return "Bradenton";
            yield return "Gilroy";
            yield return "Milford";
            yield return "Albany";
            yield return "Ankeny";
            yield return "La Crosse";
            yield return "Burlington";
            yield return "DeSoto";
            yield return "Harrisonburg";
            yield return "Minnetonka";
            yield return "Elkhart";
            yield return "Lakewood";
            yield return "Glendora";
            yield return "Southaven";
            yield return "Charleston";
            yield return "Joplin";
            yield return "Enid";
            yield return "Palm Beach Gardens";
            yield return "Brookhaven";
            yield return "Plainfield";
            yield return "Grand Island";
            yield return "Palm Desert";
            yield return "Huntersville";
            yield return "Tigard";
            yield return "Lenexa";
            yield return "Saginaw";
            yield return "Kentwood";
            yield return "Doral";
            yield return "Apple Valley";
            yield return "Grapevine";
            yield return "Aliso Viejo";
            yield return "Sammamish";
            yield return "Casa Grande";
            yield return "Pinellas Park";
            yield return "Troy";
            yield return "West Sacramento";
            yield return "Burien";
            yield return "Commerce City";
            yield return "Monroe";
            yield return "Cerritos";
            yield return "Downers Grove";
            yield return "Coral Gables";
            yield return "Wilson";
            yield return "Niagara Falls";
            yield return "Poway";
            yield return "Edina";
            yield return "Cuyahoga Falls";
            yield return "Rancho Santa Margarita";
            yield return "Harrisburg";
            yield return "Huntington";
            yield return "La Mirada";
            yield return "Cypress";
            yield return "Caldwell";
            yield return "Logan";
            yield return "Galveston";
            yield return "Sheboygan";
            yield return "Middletown";
            yield return "Murray";
            yield return "Roswell";
            yield return "Parker";
            yield return "Bedford";
            yield return "East Lansing";
            yield return "Methuen";
            yield return "Covina";
            yield return "Alexandria";
            yield return "Olympia";
            yield return "Euclid";
            yield return "Mishawaka";
            yield return "Salina";
            yield return "Azusa";
            yield return "Newark";
            yield return "Chesterfield";
            yield return "Leesburg";
            yield return "Dunwoody";
            yield return "Hattiesburg";
            yield return "Roseville";
            yield return "Bonita Springs";
            yield return "Portage";
            yield return "St. Louis Park";
            yield return "Collierville";
            yield return "Middletown";
            yield return "Stillwater";
            yield return "East Providence";
            yield return "Lawrence";
            yield return "Wauwatosa";
            yield return "Mentor";
            yield return "Ceres";
            yield return "Cedar Hill";
            yield return "Mansfield";
            yield return "Binghamton";
            yield return "Coeur d'Alene";
            yield return "San Luis Obispo";
            yield return "Minot";
            yield return "Palm Springs";
            yield return "Pine Bluff";
            yield return "Texas City";
            yield return "Summerville";
            yield return "Twin Falls";
            yield return "Jeffersonville";
            yield return "San Jacinto";
            yield return "Madison";
            yield return "Altoona";
            yield return "Columbus";
            yield return "Beavercreek";
            yield return "Apopka";
            yield return "Elmhurst";
            yield return "Maricopa";
            yield return "Farmington";
            yield return "Glenview";
            yield return "Cleveland Heights";
            yield return "Draper";
            yield return "Lincoln";
            yield return "Sierra Vista";
            yield return "Lacey";
            yield return "Biloxi";
            yield return "Strongsville";
            yield return "Barnstable Town";
            yield return "Wylie";
            yield return "Sayreville";
            yield return "Kannapolis";
            yield return "Charlottesville";
            yield return "Littleton";
            yield return "Titusville";
            yield return "Hackensack";
            yield return "Newark";
            yield return "Pittsfield";
            yield return "York";
            yield return "Lombard";
            yield return "Attleboro";
            yield return "DeKalb";
            yield return "Blacksburg";
            yield return "Dublin";
            yield return "Haltom City";
            yield return "Lompoc";
            yield return "El Centro";
            yield return "Danville";
            yield return "Jefferson City";
            yield return "Cutler Bay";
            yield return "Oakland Park";
            yield return "North Miami Beach";
            yield return "Freeport";
            yield return "Moline";
            yield return "Coachella";
            yield return "Fort Pierce";
            yield return "Smyrna";
            yield return "Bountiful";
            yield return "Fond du Lac";
            yield return "Everett";
            yield return "Danville";
            yield return "Keller";
            yield return "Belleville";
            yield return "Bell Gardens";
            yield return "Cleveland";
            yield return "North Lauderdale";
            yield return "Fairfield";
            yield return "Salem";
            yield return "Rancho Palos Verdes";
            yield return "San Bruno";
            yield return "Concord";
            yield return "Burlington";
            yield return "Apex";
            yield return "Midland";
            yield return "Altamonte Springs";
            yield return "Hutchinson";
            yield return "Buffalo Grove";
            yield return "Urbandale";
            yield return "State College";
            yield return "Urbana";
            yield return "Plainfield";
            yield return "Manassas";
            yield return "Bartlett";
            yield return "Kearny";
            yield return "Oro Valley";
            yield return "Findlay";
            yield return "Rohnert Park";
            yield return "Westfield";
            yield return "Linden";
            yield return "Sumter";
            yield return "Wilkes-Barre";
            yield return "Woonsocket";
            yield return "Leominster";
            yield return "Shelton";
            yield return "Brea";
            yield return "Covington";
            yield return "Rockwall";
            yield return "Meridian";
            yield return "Riverton";
            yield return "St. Cloud";
            yield return "Quincy";
            yield return "Morgan Hill";
            yield return "Warren";
            yield return "Edmonds";
            yield return "Burleson";
            yield return "Beverly";
            yield return "Mankato";
            yield return "Hagerstown";
            yield return "Prescott";
            yield return "Campbell";
            yield return "Cedar Falls";
            yield return "Beaumont";
            yield return "La Puente";
            yield return "Crystal Lake";
            yield return "Fitchburg";
            yield return "Carol Stream";
            yield return "Hickory";
            yield return "Streamwood";
            yield return "Norwich";
            yield return "Coppell";
            yield return "San Gabriel";
            yield return "Holyoke";
            yield return "Bentonville";
            yield return "Florence";
            yield return "Peachtree Corners";
            yield return "Brentwood";
            yield return "Bozeman";
            yield return "New Berlin";
            yield return "Goose Creek";
            yield return "Huntsville";
            yield return "Prescott Valley";
            yield return "Maplewood";
            yield return "Romeoville";
            yield return "Duncanville";
            yield return "Atlantic City";
            yield return "Clovis";
            yield return "The Colony";
            yield return "Culver City";
            yield return "Marlborough";
            yield return "Hilton Head Island";
            yield return "Moorhead";
            yield return "Calexico";
            yield return "Bullhead City";
            yield return "Germantown";
            yield return "La Quinta";
            yield return "Lancaster";
            yield return "Wausau";
            yield return "Sherman";
            yield return "Ocoee";
            yield return "Shakopee";
            yield return "Woburn";
            yield return "Bremerton";
            yield return "Rock Island";
            yield return "Muskogee";
            yield return "Cape Girardeau";
            yield return "Annapolis";
            yield return "Greenacres";
            yield return "Ormond Beach";
            yield return "Hallandale Beach";
            yield return "Stanton";
            yield return "Puyallup";
            yield return "Pacifica";
            yield return "Hanover Park";
            yield return "Hurst";
            yield return "Lima";
            yield return "Marana";
            yield return "Carpentersville";
            yield return "Oakley";
            yield return "Huber Heights";
            yield return "Lancaster";
            yield return "Montclair";
            yield return "Wheeling";
            yield return "Brookfield";
            yield return "Park Ridge";
            yield return "Florence";
            yield return "Roy";
            yield return "Winter Garden";
            yield return "Chelsea";
            yield return "Valley Stream";
            yield return "Spartanburg";
            yield return "Lake Oswego";
            yield return "Friendswood";
            yield return "Westerville";
            yield return "Northglenn";
            yield return "Phenix City";
            yield return "Grove City";
            yield return "Texarkana";
            yield return "Addison";
            yield return "Dover";
            yield return "Lincoln Park";
            yield return "Calumet City";
            yield return "Muskegon";
            yield return "Aventura";
            yield return "Martinez";
            yield return "Greenfield";
            yield return "Apache Junction";
            yield return "Monrovia";
            yield return "Weslaco";
            yield return "Keizer";
            yield return "Spanish Fork";
            yield return "Beloit";
            yield return "Panama City";
            yield return "Xalapa";
            yield return "Xiamen";
            yield return "Xi'an";
            yield return "Xianfang";
            yield return "Zurich";
            yield return "Zanzibar";
            yield return "Zagreb";
        }
    }
}