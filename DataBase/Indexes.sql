--SpecificProductInfo

CREATE INDEX IX_SpecificProductInfo_lock_type_id on SpecificProductInfo(lock_type_id)
CREATE INDEX IX_SpecificProductInfo_shape_type_id on SpecificProductInfo(shape_type_id)

--Products

CREATE INDEX IX_Products_category_id on Products(category_id)
include(id,name,image,description,disabled)

CREATE INDEX IX_Products_creator_id on Products(creator_id)
include(id,name,image,description,disabled)

CREATE INDEX IX_Products_specific_product_info_id on Products(specific_product_info_id)
include(id,name,image,description,disabled)

--SelectedProducts

CREATE INDEX IX_SelectedProducts_category_id on SelectedProducts(status_id)
include(id,count,last_status_changed)

CREATE INDEX IX_SelectedProducts_user_id on SelectedProducts(user_id)
include(id,count,last_status_changed)

CREATE INDEX IX_SelectedProducts_product_id on SelectedProducts(product_id)
include(id,count,last_status_changed)

CREATE INDEX IX_SelectedProducts_size_id on SelectedProducts(size_id)
include(id,count,last_status_changed)

--Discount

CREATE INDEX IX_Discount_product_id on Discount(product_id)
include(id,start,[end],[percent])

--StoneInfo


CREATE INDEX IX_StoneInfo_stone_type_id on StoneInfo(stone_type_id)
include(product_id,weight_carat,count)

CREATE INDEX IX_StoneInfo_stone_product_id on StoneInfo(product_id)
include(weight_carat,count)

CREATE INDEX IX_StoneInfo_stone_shape_id on StoneInfo(stone_shape_id)
include(product_id,weight_carat,count)

CREATE INDEX IX_StoneInfo_stone_color_id on StoneInfo(stone_color_id)
include(product_id,weight_carat,count)

--MaterialInfo

CREATE INDEX IX_MaterialInfo_product_id on MaterialInfo(product_id)
include([percent])

CREATE INDEX IX_MaterialInfo_material_id on MaterialInfo(material_id)
include([percent])

CREATE INDEX IX_MaterialInfo_material_color_id on MaterialInfo(material_color_id)
include([percent])

--SizeInfo

CREATE INDEX IX_SizeInfo_material_color_id on SizeInfo(product_id)
include(cost,weight_gram,count)

CREATE INDEX IX_SizeInfo_material_size_id on SizeInfo(size_id)
include(cost,weight_gram,count)