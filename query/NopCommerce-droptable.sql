DROP TABLE ShoppingCartItem
DROP TABLE OrderNote
DROP TABLE RecurringPayment
DROP TABLE RewardPointsHistory
DROP TABLE [Order]
DROP TABLE PredefinedProductAttributeValue
DROP TABLE RelatedProduct
DROP TABLE ProductAttribute
DROP TABLE Picture
DROP TABLE Product_Category_Mapping
DROP TABLE Product
DROP TABLE PredefinedProductAttributeValue
DROP TABLE Product_Picture_Mapping
DROP TABLE Product_ProductAttribute_Mapping
DROP TABLE PredefinedProductAttributeValue
DROP TABLE Product_ProductTag_Mapping
DROP TABLE Product_SpecificationAttribute_Mapping
DROP TABLE ProductReview
DROP TABLE ProductReviewHelpfulness
DROP TABLE GiftCard
drop table Address
drop table AddressAttribute
drop table Categorynop
drop table CheckoutAttribute
drop table Country
drop table Customer
drop table  	PollAnswer
drop table  	CustomerAttribute
drop table  	CustomerRole
drop table  	Discount
drop table  	Shipment
drop table  	ShippingMethod
drop table  	SpecificationAttribute
drop table  	StateProvince
drop table  	Forums_Forum
drop table  	Store
drop table  	Forums_Group
drop table  	Forums_Post
drop table  	Forums_Topic
drop table  	Vendor
drop table  	[Language]
drop table  	OrderItem
drop table  	PermissionRecord
drop table  	category
drop table  	Poll

Delete UrlRecord Where EntityName = 'Product'

DBCC CHECKIDENT (ShoppingCartItem, RESEED, 0)
DBCC CHECKIDENT (OrderNote, RESEED, 0)
DBCC CHECKIDENT (RecurringPayment, RESEED, 0)
DBCC CHECKIDENT (RewardPointsHistory, RESEED, 0)
DBCC CHECKIDENT ([Order], RESEED, 0)
DBCC CHECKIDENT (RelatedProduct, RESEED, 0)
DBCC CHECKIDENT (ProductAttribute, RESEED, 0)
DBCC CHECKIDENT (Picture, RESEED, 0)
DBCC CHECKIDENT (Product_Category_Mapping, RESEED, 0)
DBCC CHECKIDENT (Product, RESEED, 0)
DBCC CHECKIDENT (PredefinedProductAttributeValue, RESEED, 0)
DBCC CHECKIDENT (Product_Picture_Mapping, RESEED, 0)
DBCC CHECKIDENT (Product_ProductAttribute_Mapping, RESEED, 0)
DBCC CHECKIDENT (Product_SpecificationAttribute_Mapping, RESEED, 0)
DBCC CHECKIDENT (PredefinedProductAttributeValue, RESEED, 0)
DBCC CHECKIDENT (ProductReview, RESEED, 0)
DBCC CHECKIDENT (ProductReviewHelpfulness, RESEED,0)