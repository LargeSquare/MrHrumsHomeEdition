--Food

create table [FoodName]
(
	Id int primary key identity not null,
	Name nvarchar(50) not null,
	Visible bit default 1 not null
);
go

create table [FoodWeight]
(
	Id int primary key identity not null,
	Weight float not null,
	Visible bit default 1 not null
);
go

create table [Granule]
(
	Id int primary key identity not null,
	Size nvarchar(50) not null,
	Visible bit default 1 not null
);
go

create table [Price]
(
	Id int primary key identity not null,
	Retail decimal default 0 not null,
	Kennel decimal default 0 not null,
	Purchase decimal default 0 not null,
	ForSelf decimal default 0 not null,
	Visible bit default 1 not null
);
go


create table [Food]
(
	Id int primary key identity not null,
	FoodNameID int not null,
	FoodWeightID int not null,
	GranuleID int not null,
	PriceID int not null,
	Visible bit default 1 not null
);
go

alter table [Food] add constraint [FK_Food_FoodNameID]
	foreign key (FoodNameID) references [FoodName]([Id]);
go
	
alter table [Food] add constraint [FK_Food_FoodWeightID]
	foreign key (FoodWeightID) references [FoodWeight]([Id]);
go
	
alter table [Food] add constraint [FK_Food_GranuleID]
	foreign key (GranuleID) references [Granule]([Id]);
go
	
alter table [Food] add constraint [FK_Food_PriceID]
	foreign key (PriceID) references [Price]([Id]);
go
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	