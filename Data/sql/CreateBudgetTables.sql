--Budget

create table [TypeOfBudgetAction]
(
	Id int primary key identity not null,
	Name nvarchar(50) not null unique
);
go

create table [BudgetState]
(
	Id int primary key identity not null,
	Name nvarchar(50) not null,
	TypeOfBudgetActionID int not null,
	Visible bit not null default 1
);
go

create table [Budget]
(
	Id int primary key identity not null,
	Amount money not null,
	BudgetStateID int not null,
	Date datetime not null default (GetDate()),
	Note nvarchar null
);
go


alter table [BudgetState] add constraint [FK_BudgetState_TypeOfBudgetActionID]
	foreign key ([TypeOfBudgetActionID]) references [TypeOfBudgetAction]([Id]);
	go

alter table [Budget] add constraint [FK_Budget_BudgetStateID]
	foreign key ([BudgetStateID]) references [BudgetState]([Id]);
	go



