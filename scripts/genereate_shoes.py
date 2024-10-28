import random

sizes = ['36', '37', '38', '39', '40', '41', '42', '43', '44', '45', '46']
colors = ["Black", "White", "Blue", "Red", "Gray", "Green", "Brown", "Navy", "Beige"]
shoe_producers = [
    'Nike', 'Adidas', 'Reebok', 'Under Armour', 'Asics', 'New Balance', 
    'Brooks', 'Hoka One One', 'Saucony', 'Salomon', 'Merrell', 'Puma', 
    'Vans', 'Converse', 'Mizuno', 'Altra', 'Karhu', 'Topo Athletic', 
    'On', 'Scott'
]
shoe_series = [
    "Air_Max", "Air_Force", "React_Infinity", "Zoom_Fly", "Pegasus", "Free RN", 
    "Joyride", "VaporMax", "Ultraboost", "NMD", "Superstar", "Stan_Smith", 
    "Yeezy_Boost", "Gazelle", "Samba", "Terrex", "Classic", "Nano", "Zig_Kinetica", 
    "Floatride", "HOVR", "Charged", "SpeedForm", "Gel_Kayano", "Gel_Nimbus", 
    "GT_2000", "990", "Fresh_Foam", "574", "Ghost", "Adrenaline", "Glycerin", 
    "Bondi", "Clifton", "Arahi", "Triumph", "Kinvara", "Guide", "Speedcross", 
    "XA_Pro", "Moab", "Trail_Glove", "RSX", "Ignite", "Cell", "Old_Skool", 
    "Authentic", "Sk8_Hi", "Chuck_Taylor", "One_Star", "Wave_Rider", "Wave_Inspire", 
    "Escalante", "Torin", "Lone_Peak", "Fusion", "Ikoni", "Phantom", "MT3", 
    "Cloudstratus", "Cloudflyer", "Supertrac", "Trail_Rocket", "ZoomX_Vaporfly", 
    "Air_Zoom_Terra_Kiger", "EQT", "ZX_Flux", "Pump", "InstaPump_Fury", 
    "Speedform_Gemini", "MetaRun", "1080", "Ravenna", "Speedgoat", "Peregrine", 
    "S_LAB", "Jungle_Moc", "Faas", "Slip_On", "Jack_Purcell", "Wave_Sky", 
    "Paradigm", "Aria", "Fli_Lyte", "Gravel", "SB Dunk", "Air_Max_97", 
    "Yeezy350", "Workout_Plus", "Project_Rock", "GT1000", "860", "Launch", 
    "Cavu", "Cohesion", "XUltra", "Moab_2", "Deviate_Nitro"
]
stock_range = (0, 20)

unique_skus = set()

def generate_sku():
    producer = random.choice(shoe_producers)
    series = random.choice(shoe_series)
    color = random.choice(colors)
    size = random.choice(sizes)
    return f"{producer}-{series}-{color}-{size}"

while len(unique_skus) < 1000:
    sku = generate_sku()
    unique_skus.add(sku)

with open("insert_shoes.sql", "w") as file:
    file.write(
        """ DROP TABLE IF EXISTS shoes;
            CREATE TABLE shoes (
                sku VARCHAR(255) PRIMARY KEY,
                stock INT
            );
        """
    )
    file.write("INSERT INTO shoes (sku, stock) VALUES\n")

    sku_list = []
    for sku in unique_skus:
        stock = random.randint(*stock_range)
        sku_list.append(f"('{sku}', {stock})")
    
    file.write(",\n".join(sku_list) + ";\n\n")

    file.write("SELECT * FROM shoes;")
