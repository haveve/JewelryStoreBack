CREATE DATABASE JewelryStore
GO

--USER

USE JewelryStore 
GO

CREATE TABLE Users(
id int identity not null,
full_name nvarchar(100) not null,
telephone_number nvarchar(10) not null CHECK(telephone_number like '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
disabled bit null,
password nvarchar(50) not null,
salt nvarchar(24) not null,

CONSTRAINT PK_Users_id Primary Key(id),
CONSTRAINT UQ_Users_telephone_number UNIQUE(telephone_number)
)

CREATE TABLE UserPermission(
user_id int not null,
product_manage bit not null,
user_manage bit not null,

CONSTRAINT FK_UserPermission_user_id FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
CONSTRAINT PK_UsersPermissiom_user_id Primary Key(user_id)
);

-- SpecificProductInfo

CREATE TABLE LockType(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_LockType_id PRIMARY KEY(id),
CONSTRAINT UQ_LockType_name UNIQUE(name)
)


CREATE TABLE ShapeType(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_ShapeType_id PRIMARY KEY(id),
CONSTRAINT UQ_ShapeType_name UNIQUE(name)
)

CREATE TABLE SpecificProductInfo(
id int identity not null,
lock_type_id int not null,
shape_type_id int not null,

CONSTRAINT FK_SpecificProductInfo_lock_type_id FOREIGN KEY (lock_type_id) REFERENCES LockType(id) ON DELETE NO ACTION,
CONSTRAINT FK_SpecificProductInfo_shape_type_id FOREIGN KEY (shape_type_id) REFERENCES ShapeType(id) ON DELETE NO ACTION,
CONSTRAINT PK_SpecificProductInfo_id Primary Key(id)
CONSTRAINT UQ_SpecificProductInfo_lock_shape_types_id UNIQUE(lock_type_id,shape_type_id)
)



--PRODUCT

CREATE TABLE ProductCreator(
id int not null,
name nvarchar(100) not null,
disabled bit null,

CONSTRAINT PK_ProductCreator_id Primary Key(id),
CONSTRAINT UQ_ProductCreator_name UNIQUE(name)
);

CREATE TABLE ProductCategory(
id int not null,
name nvarchar(100) not null

CONSTRAINT PK_ProductCategory_id Primary Key(id),
CONSTRAINT UQ_ProductCategory_name UNIQUE(name)
);

CREATE TABLE Products(
id int identity not null,
name nvarchar(100) not null,
image nvarchar(100) not null,
description nvarchar(300) not null,
disabled bit null,
category_id int null,
creator_id int not null,
specific_product_info_id int null,

CONSTRAINT FK_Products_creator_id FOREIGN KEY (creator_id) REFERENCES ProductCreator(id) ON DELETE NO ACTION,
CONSTRAINT FK_Products_category_id FOREIGN KEY (category_id) REFERENCES ProductCategory(id) ON DELETE SET NULL,
CONSTRAINT FK_Products_specific_product_info_id FOREIGN KEY(specific_product_info_id) REFERENCES SpecificProductInfo(id) ON DELETE SET NULL,
CONSTRAINT UQ_Products_name UNIQUE(name),
CONSTRAINT PK_Product_id Primary Key(id)
)

--USER HISTORY

CREATE TABLE History(
id uniqueidentifier not null,
date datetime2(0),
total_cost decimal(10,2) not null,
address nvarchar(200) not null

CONSTRAINT PK_History_id PRIMARY KEY(id)
)

--SELECTED PRODUCT

CREATE TABLE SelectedProductsStatus(
id int not null,
name nvarchar(50) not null

CONSTRAINT PK_SelectedProductsStatus_id PRIMARY KEY(id),
CONSTRAINT UQ_SelectedProductsStatus_name UNIQUE(name)
)


CREATE TABLE SelectedProducts(
id uniqueidentifier not null,
count int not null,
last_status_changed datetime2(0),

status_id int not null,
user_id int not null,
product_id int not null,
size_id int not null,

CONSTRAINT FK_SelectedProducts_user_id FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE NO ACTION,
CONSTRAINT FK_SelectedProducts_product_id FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT FK_SelectedProducts_status_id FOREIGN KEY (status_id) REFERENCES SelectedProductsStatus(id) ON DELETE NO ACTION,
CONSTRAINT FK_SelectedProducts_size_id FOREIGN KEY(size_id) REFERENCES Size(id) ON DELETE NO ACTION,

CONSTRAINT PK_SelectedProducts_id PRIMARY KEY(id,size_id,product_id)
)

--ORDERs

CREATE TABLE Orders(
id uniqueidentifier not null,
count int not null,
last_status_changed datetime2(0),

user_id int not null,
product_id int not null,
size_id int not null,

CONSTRAINT FK_Orders_user_id FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE NO ACTION,
CONSTRAINT FK_Orders_product_id FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT FK_Orders_size_id FOREIGN KEY(size_id) REFERENCES Size(id) ON DELETE NO ACTION,
CONSTRAINT FK_Orders_history FOREIGN KEY(id) REFERENCES History(id) ON DELETE NO ACTION,

CONSTRAINT PK_Orders_id PRIMARY KEY(id,size_id,product_id)
)

--DISCOUNT

CREATE TABLE Discount(
id int not null identity;
start date not null,
[end] date not null,
product_id int not null,
[percent] int not null CHECK([percent] <= 100 AND [percent] > 0)

CONSTRAINT FK_Discount_product_id FOREIGN KEY(product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT UQ_Discount_product_time UNIQUE(product_id,start,[end])
CONSTRAINT PK_Discount_id PRIMARY KEY(id) 
);

--STONE INFO
CREATE TABLE StoneColor(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_StoneColor_id PRIMARY KEY(id),
CONSTRAINT UQ_StoneColor_name UNIQUE(name)
);


CREATE TABLE StoneType(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_StoneType_id PRIMARY KEY(id),
CONSTRAINT UQ_StoneType_name UNIQUE(name)

);

CREATE TABLE StoneShape(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_StoneShape_id PRIMARY KEY(id),
CONSTRAINT UQ_StoneShape_name UNIQUE(name)

);

CREATE TABLE StoneInfo(
product_id int not null,
stone_type_id int not null,
stone_shape_id int not null,
stone_color_id int not null,

weight_carat decimal(4,1) not null,
count int not null,

CONSTRAINT FK_StoneInfo_stone_type_id FOREIGN KEY (stone_type_id) REFERENCES StoneType(id) ON DELETE NO ACTION,
CONSTRAINT FK_StoneInfo_product_id FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT FK_StoneInfo_stone_shape_id FOREIGN KEY (stone_shape_id) REFERENCES StoneShape(id) ON DELETE NO ACTION,
CONSTRAINT FK_StoneInfo_stone_color_id FOREIGN KEY (stone_color_id) REFERENCES StoneColor(id) ON DELETE NO ACTION,

CONSTRAINT PK_StoneInfo_product_stone_t_stone_s_ids PRIMARY KEY(product_id,stone_type_id,stone_shape_id,stone_color_id,weight_carat)
)

--Material Info

CREATE TABLE MaterialColor(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_MaterialColor_id PRIMARY KEY(id),
CONSTRAINT UQ_MaterialColor_name UNIQUE(name)
)

CREATE TABLE Material(
id int not null,
name nvarchar(50) not null,

CONSTRAINT PK_Material_id PRIMARY KEY(id),
CONSTRAINT UQ_Material_name UNIQUE(name)

);

CREATE TABLE MaterialInfo(
product_id int not null,
material_id int not null,
material_color_id int not null,
[percent] int not null,

CONSTRAINT FK_MaterialInfo_material_id FOREIGN KEY (material_id) REFERENCES Material(id) ON DELETE NO ACTION,
CONSTRAINT FK_MaterialInfo_product_id FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT FK_MaterialInfo_materal_color_id FOREIGN KEY (materal_color_id) REFERENCES MaterialColor(id) ON DELETE NO ACTION,
CONSTRAINT PK_MaterialInfo_product_material_id PRIMARY KEY(product_id,material_id,materal_color_id)
)

--Size Info

CREATE TABLE Size(
id int not null,
name nvarchar(5) not null,

CONSTRAINT PK_Size_id PRIMARY KEY(id),
CONSTRAINT UQ_Size_name UNIQUE(name)
);

CREATE TABLE SizeInfo(
product_id int not null,
size_id int not null,
cost decimal(10,2) not null,
weight_gram decimal(5,1) not null,
count int not null

CONSTRAINT FK_SizeInfo_size_id FOREIGN KEY (size_id) REFERENCES Size(id) ON DELETE NO ACTION,
CONSTRAINT FK_SizeInfo_product_id FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE NO ACTION,
CONSTRAINT UQ_SizeInfo_product_id_cost UNIQUE (cost,product_id),


CONSTRAINT PK_SizeInfo_product_size_id PRIMARY KEY(product_id,size_id)
)