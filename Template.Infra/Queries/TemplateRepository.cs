namespace Template.Infra.Repositories
{
    public partial class TemplateRepository
	{
		//CREATE TABLE template.dbo.template (
		//	Id int IDENTITY(1,1) NOT NULL,
		//	Name varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		//	Property1 varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		//	Property2 varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
		//	CriadoPor int NOT NULL,
		//	DataCriacao datetime NOT NULL,
		//	AlteradoPor int NULL,
		//	DataAlteracao datetime NULL,
		//	Deletado bit DEFAULT 0 NOT NULL,
		//	CONSTRAINT PK__template__3214EC07C3B1E5F8 PRIMARY KEY (Id)
		//);

		private const string _GetByName = @"
		select
			Id,
			Name,
			Property1,
			Property2
		from
			[Template] t
		where
			Name = @name";

		private const string _GetById = @"
		select
			Id,
			Name,
			Property1,
			Property2
		from
			Template
		where
			1 = 1
			and Id = @id
			and Deletado = 0";

		private const string _GetAll = @"
		select
			Id,
			Name,
			Property1,
			Property2
		from
			Template 
		where
			1 = 1
			and Deletado = 0";

		private const string _Insert = @"
		INSERT
			INTO
			Template (
			Name,
			Property1,
			Property2,
			CriadoPor,
			DataCriacao,
			AlteradoPor,
			DataAlteracao,
			Deletado)
			output inserted.id
		VALUES
			(@Name,
			@Property1,
			@Property2,
			@CriadoPor,
			GETDATE(),
			NULL,
			NULL,
			0);";

		private const string _Update = @"
		UPDATE
			Template
		SET
			Name = @Name,
			Property1 = @Property1,
			Property2 = @Property2,
			AlteradoPor = @AlteradoPor,
			DataAlteracao = GETDATE()
		WHERE
			Id = @Id;";

		private const string _Delete = @"
		UPDATE
			Template
		SET			
			Deletado = 1,
			AlteradoPor = @AlteradoPor,
			DataAlteracao = GETDATE()
		WHERE
			Id = @id;";
	}
}
