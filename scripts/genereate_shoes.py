import random

brand_to_series = {
    'Nike': [
        "Air_Max", "Air_Force", "React_Infinity", "Zoom_Fly", "Pegasus", "Free_RN",
        "Joyride", "VaporMax", "SB Dunk", "ZoomX_Vaporfly", "Air_Zoom_Terra_Kiger", 
        "Air_Max_97"
    ],
    'Adidas': [
        "Ultraboost", "NMD", "Superstar", "Stan_Smith", "Yeezy_Boost", "Yeezy350",
        "Gazelle", "Samba", "EQT", "ZX_Flux"
    ],
    'Reebok': [
        "Classic", "Nano", "Zig_Kinetica", "Floatride", "Pump", "InstaPump_Fury", 
        "Workout_Plus"
    ],
    'Under Armour': [
        "HOVR", "Charged", "SpeedForm", "Project_Rock", "Speedform_Gemini"
    ],
    'Asics': [
        "Gel_Kayano", "Gel_Nimbus", "GT_2000", "MetaRun", "GT1000"
    ],
    'New Balance': [
        "990", "Fresh_Foam", "574", "1080", "860"
    ],
    'Brooks': [
        "Ghost", "Adrenaline", "Glycerin", "Launch", "Ravenna"
    ],
    'Hoka One One': [
        "Bondi", "Clifton", "Arahi", "Cavu", "Speedgoat"
    ],
    'Saucony': [
        "Triumph", "Kinvara", "Guide", "Peregrine", "Cohesion"
    ],
    'Salomon': [
        "Speedcross", "XA_Pro", "XUltra", "S_LAB"
    ],
    'Merrell': [
        "Moab", "Trail_Glove", "Jungle_Moc", "Moab_2"
    ],
    'Puma': [
        "RSX", "Ignite", "Cell", "Faas", "Deviate_Nitro"
    ],
    'Vans': [
        "Old_Skool", "Authentic", "Sk8_Hi", "Slip_On"
    ],
    'Converse': [
        "Chuck_Taylor", "One_Star", "Jack_Purcell"
    ],
    'Mizuno': [
        "Wave_Rider", "Wave_Inspire", "Wave_Sky"
    ],
    'Altra': [
        "Escalante", "Torin", "Lone_Peak", "Paradigm"
    ],
    'Karhu': [
        "Fusion", "Ikoni", "Aria"
    ],
    'Topo Athletic': [
        "Phantom", "MT3", "Fli_Lyte"
    ],
    'On': [
        "Cloudstratus", "Cloudflyer"
    ],
    'Scott': [
        "Supertrac", "Trail_Rocket", "Gravel"
    ]
}
sizes = ['36', '37', '38', '39', '40', '41', '42', '43', '44']
stock_range = (0, 20)
prices = ['300', '325', '350', '375', '400', '425', '450', '475', '500']

size_data = []
series_data = []

brand_counter = 1  # Initial values. Makes the SKU values start at 001.

for brand, series_list in brand_to_series.items():
    brand_code = f"{brand_counter:03}"
    series_counter = 1  # When brand changes, resets series to 1.

    for series in series_list:
        series_code = f"{series_counter:03}"
        sku = None
        
        random_price = random.choice(prices)
        series_data.append((brand, series, random_price))  # Store brand, series, price

        for size in sizes:
            sku = f"100{brand_code}{series_code}-{size}"
            random_stock = random.randint(*stock_range)
            size_data.append((sku, size, random_stock))  # Store sku, size, stock
            
        series_counter += 1

    brand_counter += 1

with open("insert_shoes.sql", "w") as file:
    file.write(""" 
DROP TABLE IF EXISTS shoes;
DROP TABLE IF EXISTS series;

CREATE TABLE series (
    id INT IDENTITY(1,1) PRIMARY KEY,
    price DECIMAL(10, 2) NOT NULL,
    brand VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL
);

GO

CREATE TABLE shoes (
    sku VARCHAR(255) PRIMARY KEY,
    stock INT NOT NULL,
    size INT NOT NULL,
    series_id INT NOT NULL,
    FOREIGN KEY (series_id) REFERENCES series(id) ON DELETE CASCADE
);

GO
    \n""")

    file.write("INSERT INTO series (brand, name, price) VALUES\n")
    series_values = [f"('{brand}', '{series}', {price})" for brand, series, price in series_data]
    file.write(",\n".join(series_values) + ";\n\n")                 # replace the last ',' with ';'

    file.write("INSERT INTO shoes (sku, size, stock, series_id) VALUES")
    
    shoes = []
    for entry, (sku, size, stock) in enumerate(size_data):          # Since every shoe series has the same num of sizes, 
        series_id = (entry // len(sizes)) + 1                       # we can take the entries indices in the size_data collection,                       
        shoes.append(f"('{sku}', {size}, {stock}, {series_id})")    # and divide them by the length of the collection, to get the correct series_id.
                                                                    # Add 1 to offset the 0 indexing.                           
    file.write(",\n".join(shoes) + ";\n\n")                         # I.e. if we have 5 sizes, we know index 0-4 will have the same series,
                                                                    # and index 5-9 will have the next series etc.                               
    file.write("SELECT * FROM shoes;\n")
    file.write("SELECT * FROM series;\n")
