use gestao;
DECLARE @COD_LOJA AS INTEGER
DECLARE @COD_LISTA AS INTEGER

insert into Listas values ((select top 1 codigo + 1 from listas order by codigo desc), 'teLevo', 0, 1,0,7)

SET @COD_LOJA = 1 --CODIGO DA LOJA QUE DESEJA ATIVAR 
SET @COD_LISTA = (select top 1 codigo from listas order by codigo desc ) --CODIGO DA LISTA DE PRODUTOS QUE A SINCRONIZACOO IRA UTILIZAR

UPDATE Lojas SET CodListaDrive = @COD_LISTA, SincronizaDrive = 1
WHERE CODIGO =@COD_LOJA

delete from DriveTiposExportacao 
Insert Into DriveTiposExportacao Values(1, 'Atualização de Produtos')
Insert Into DriveTiposExportacao Values(2, 'Exclusão de Produtos')
Insert Into DriveTiposExportacao Values(3, 'Alteração de Status do Pedidos')
Insert Into DriveTiposExportacao Values(4, 'Inclusão de Produto')

insert into GRUPOS_CLIENTES (codigo, nome,cd_estrutural,VendaCartaoFidelidade ,VendaCheque ,ParticipaCampanhasPromocionais, JUROS_ATRASO, DIAS_TOLERANCIA,SOBRE_RENDA, DIAS_TOLERANCIA_JUROS  )
values
((select top 1 codigo + 1 from GRUPOS_CLIENTES order by codigo desc), 'teLevo',
(select top 1 substring(('0' + Convert(nvarchar(5),(codigo + 1))),len('0' + Convert(nvarchar(5),(codigo + 1)))-1,2) as estrutura from GRUPOS_CLIENTES order by codigo desc),
0,0,1,0,0,0,0)

insert into Configuracoes values ((select top 1 codigo + 1 from Configuracoes order by codigo desc), 'LISTA_PRODUTOS_EXPORTACAO_DRIVE',@COD_LISTA, null )

insert into OrigemPedido values ((select top 1 codigo + 1 from OrigemPedido order by codigo desc), 'teLevo', 1, 1)

GO 
create TRIGGER [dbo].[TG_EXPORTACAO_TELEVO]    
                     ON [dbo].[Produtos]    
                     FOR DELETE, INSERT, UPDATE    
                     AS   
                      SET NOCOUNT ON;    
                      
                     DECLARE @TEXTO AS NVARCHAR(100);           
					 DECLARE @CODIGOI AS FLOAT ; 
                     DECLARE @DESCRICAOI AS NVARCHAR(100); 
					 DECLARE @NCMI AS NVARCHAR(20); 
                     DECLARE @CONFIGI AS NVARCHAR(50); 
                     DECLARE @CODIGOD AS FLOAT ; 
                     DECLARE @DESCRICAOD AS NVARCHAR(100); 
					 DECLARE @NCMD AS NVARCHAR(20); 
                     DECLARE @CONFIGD AS NVARCHAR(50);                       
                     DECLARE @IS_UPDATE_INSERT AS BIT; 
                     DECLARE @IS_DELETE AS BIT; 
                     DECLARE @LISTA_CONFIG AS FLOAT; 
                      
                     SET @IS_UPDATE_INSERT= 0; 
               
                     IF ((EXISTS (SELECT * FROM   INSERTED) AND EXISTS (SELECT * FROM   DELETED))  
                         OR (EXISTS (SELECT * FROM   INSERTED) AND NOT EXISTS (SELECT * FROM   DELETED))) 
                         BEGIN 
                             SET @IS_UPDATE_INSERT = (1);       
                         END 
               
                     IF (@IS_UPDATE_INSERT = 1 ) 
                         BEGIN 
                             DECLARE NOVO_DRIVE CURSOR  
                             FOR SELECT I.CODIGO as CodigoI,    
                                         I.DESCRICAO as DescricaoI,    
                                         I.CONFIG as ConfigI, 
										 I.NCM as NCMI, 
                                         ISNULL (D.CODIGO,0) AS CodigoD, 
                                         ISNULL(D.DESCRICAO,'') As DescricaoD, 
                                         ISNULL(D.Config,'0') as ConfigD ,
										 ISNULL(D.NCM,'') as NCMD 
                                         
                                 FROM      Inserted I  
                                 Left JOIN  Deleted D ON I.Codigo = D.Codigo  
                                 INNER JOIN LISTA_PRODUTOS ON I.CODIGO = LISTA_PRODUTOS.CD_PRODUTO   
                                 inner join Lojas on LISTA_PRODUTOS.CD_LISTA = lojas.CodListaDrive  
           
                                 OPEN NOVO_DRIVE;    
               
                             FETCH NEXT FROM NOVO_DRIVE INTO @CODIGOI, @DESCRICAOI, @CONFIGI, @NCMI, @CODIGOD, @DESCRICAOD, @CONFIGD, @NCMD; 
                             WHILE @@FETCH_STATUS = 0    
                             BEGIN     
                                 IF (SUBSTRING(@CONFIGI,1,1) = '1' AND (SUBSTRING(@CONFIGD,1,1) = '0' OR ( @NCMI <> @NCMD  OR @DESCRICAOI <> @DESCRICAOD OR @CODIGOI <> @CODIGOD ))) 
                                     BEGIN 
                                         INSERT INTO [dbo].[DriveFilaExportacoes] 
                                             ([CodTipoExportacao] 
                                             ,[Detalhe] 
                                             ,[DataHoraRegistro] 
                                             ,[DataHoraUltimoEnvio] 
                                             ,[MensagemErro] 
                                             ,[Exportado]) 
                                         VALUES 
                                             (1 
											 ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CODIGOI as bigint))))                                             
                                             ,getdate() 
                                             ,null 
                                             ,'' 
                                             ,0) 
                                     END 
                                 ELSE 
                                     IF (SUBSTRING(@CONFIGI,1,1) = '0' AND SUBSTRING(@CONFIGD,1,1) = '1' ) 
                                         BEGIN 
                                             INSERT INTO [dbo].[DriveFilaExportacoes] 
                                                 ([CodTipoExportacao] 
                                                 ,[Detalhe] 
                                                 ,[DataHoraRegistro] 
                                                 ,[DataHoraUltimoEnvio] 
                                                 ,[MensagemErro] 
                                                 ,[Exportado]) 
                                             VALUES 
                                                 (2 
                                                 ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CODIGOI as bigint))))
                                                 ,getdate() 
                                                 ,null 
                                                 ,'' 
                                                 ,0) 
                                         END 
                                      
                                 FETCH NEXT FROM NOVO_DRIVE INTO @CODIGOI, @DESCRICAOI, @CONFIGI, @NCMI, @CODIGOD, @DESCRICAOD, @CONFIGD, @NCMD;  
                             END    
                             CLOSE NOVO_DRIVE;    
                             DEALLOCATE NOVO_DRIVE;  
                         END 
                     ELSE 
                         BEGIN 
                             DECLARE DELETADO_DRIVE CURSOR  
                             FOR SELECT  Deleted.Codigo, 
                                         Deleted.Descricao, 
                                         Deleted.Config                          
                                 FROM Deleted     
                                 INNER JOIN LISTA_PRODUTOS ON Deleted.CODIGO = LISTA_PRODUTOS.CD_PRODUTO  
                                 inner join Lojas on LISTA_PRODUTOS.CD_LISTA = lojas.CodListaDrive                       
               
                                 OPEN DELETADO_DRIVE;    
               
                                 FETCH NEXT FROM DELETADO_DRIVE INTO @CODIGOD, @DESCRICAOD, @CONFIGD; 
                                                                                                                                                                                                                                                                                                                                                              
                             WHILE @@FETCH_STATUS = 0    
                             BEGIN     
                                 IF (SUBSTRING(@CONFIGD,1,1) = '1') 
                                     BEGIN 
                                         INSERT INTO [dbo].[DriveFilaExportacoes] 
                                             ([CodTipoExportacao] 
                                             ,[Detalhe] 
                                             ,[DataHoraRegistro] 
                                             ,[DataHoraUltimoEnvio] 
                                             ,[MensagemErro] 
                                             ,[Exportado]) 
                                         VALUES 
                                             (2 
                                             ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CODIGOI as bigint))))
                                             ,getdate() 
                                             ,null 
                                             ,'' 
                                             ,0) 
                                     END 
                                                                        
                                 FETCH NEXT FROM DELETADO_DRIVE INTO @CODIGOD, @DESCRICAOD, @CONFIGD; 
                             END    
                             CLOSE DELETADO_DRIVE;    
                             DEALLOCATE DELETADO_DRIVE;  
                         END 
            
GO


			 USE [GESTAO]
GO
/****** Object:  Trigger [dbo].[TG_EXPORTACAO_TELEVO_LISTA]    Script Date: 14/04/2020 16:08:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
			create TRIGGER [dbo].[TG_EXPORTACAO_TELEVO_LISTA]   
                     ON [dbo].[LISTA_PRODUTOS]
                     FOR DELETE, INSERT, UPDATE   
                     AS  
                      SET NOCOUNT ON;   
                     
                     DECLARE @TEXTO AS NVARCHAR(100);
                     DECLARE @CD_LISTA AS FLOAT ;
                     DECLARE @CD_PRODUTO AS float;
                     DECLARE @LISTA_CONFIG AS FLOAT;
                     DECLARE @IS_UPDATE_INSERT AS BIT;
                     
                     SET @IS_UPDATE_INSERT= 0;             
              
                     IF ((EXISTS (SELECT * FROM   INSERTED) AND EXISTS (SELECT * FROM   DELETED)) 
                         OR (EXISTS (SELECT * FROM   INSERTED) AND NOT EXISTS (SELECT * FROM   DELETED)))
                         BEGIN
                             SET @IS_UPDATE_INSERT = (1);      
                         END
                                             
                     IF (@IS_UPDATE_INSERT = 1 )
                         BEGIN
                             DECLARE NOVO_DRIVE CURSOR 
                             FOR SELECT CD_LISTA, 
                                        CD_PRODUTO
                                        
                                 FROM  INSERTED  
                                 INNER JOIN LOJAS L ON INSERTED.CD_LISTA = L.CodListaDrive           
                                               
                                 OPEN NOVO_DRIVE;   
              
                             FETCH NEXT FROM NOVO_DRIVE INTO @CD_LISTA, @CD_PRODUTO;
                                                                                                                                                                                                                                                                                                                                         
                             WHILE @@FETCH_STATUS = 0   
                             BEGIN    
                                 
                                 INSERT INTO [dbo].[DriveFilaExportacoes]
                                     ([CodTipoExportacao]
                                     ,[Detalhe]
                                     ,[DataHoraRegistro]
                                     ,[DataHoraUltimoEnvio]
                                     ,[MensagemErro]
                                     ,[Exportado])
                                 VALUES
                                     (4                                    
									 ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CD_PRODUTO as bigint))))
                                     ,getdate()
                                     ,null
                                     ,''
                                     ,0)
                                                                  
                                 FETCH NEXT FROM NOVO_DRIVE INTO @CD_LISTA, @CD_PRODUTO;
                             END   
                             CLOSE NOVO_DRIVE;   
                             DEALLOCATE NOVO_DRIVE; 
                         END
                     ELSE
                         BEGIN
                             DECLARE DELETADO_DRIVE CURSOR 
                             FOR SELECT CD_LISTA, 
                                          CD_PRODUTO
                                        
                                 FROM  DELETED     
                                 INNER JOIN LOJAS L ON DELETED.CD_LISTA = L.CodListaDrive                            
                                 --WHERE CD_LISTA = @LISTA_CONFIG
              
                                 OPEN DELETADO_DRIVE;   
              
                                 FETCH NEXT FROM DELETADO_DRIVE INTO @CD_LISTA, @CD_PRODUTO;
                             WHILE @@FETCH_STATUS = 0   
                             BEGIN    
                                 
                                         INSERT INTO [dbo].[DriveFilaExportacoes]
                                             ([CodTipoExportacao]
                                             ,[Detalhe]
                                             ,[DataHoraRegistro]
                                             ,[DataHoraUltimoEnvio]
                                             ,[MensagemErro]
                                             ,[Exportado])
                                         VALUES
                                             (2
                                             ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CD_PRODUTO as bigint))))
                                             ,getdate()
                                             ,null
                                             ,''
                                             ,0)                     
                               
                                 FETCH NEXT FROM DELETADO_DRIVE INTO @CD_LISTA, @CD_PRODUTO;
                             END   
                             CLOSE DELETADO_DRIVE;   
                             DEALLOCATE DELETADO_DRIVE; 
                         END
              
         GO     
             create TRIGGER [dbo].[TG_EXPORTACAO_TELEVO_PRODUTOSLOJAS]   
                    ON [dbo].[ProdutoLojas]   
                    FOR  INSERT, UPDATE   
                    AS  
                     SET NOCOUNT ON;   
                    
                    DECLARE @TEXTO AS NVARCHAR(100);             
                    DECLARE @COD_LOJA AS FLOAT ;
                    DECLARE @CD_PRODUTO AS FLOAT;
                    DECLARE @CONFIG AS NVARCHAR(50);
                    DECLARE @VALORNOPDV AS MONEY ;
                    DECLARE @VALORNOPDV_D AS MONEY ;                    
                    DECLARE @SincronizaDrive AS BIT
                    DECLARE @IS_UPDATE_INSERT AS BIT;
                    DECLARE @IS_DELETE AS BIT;
                    DECLARE @LISTA_CONFIG AS FLOAT;                    
                    SET @IS_UPDATE_INSERT= 0;
                    set @SincronizaDrive = 0;
                            
                    IF ((EXISTS (SELECT * FROM   INSERTED) AND EXISTS (SELECT * FROM   DELETED)) 
                        OR (EXISTS (SELECT * FROM   INSERTED) AND NOT EXISTS (SELECT * FROM   DELETED)))
                        BEGIN
                            SET @IS_UPDATE_INSERT = (1);      
                        END
                          
                    IF (@IS_UPDATE_INSERT = 1 )
                        BEGIN
                            DECLARE NOVO_DRIVE CURSOR 
                            FOR SELECT I.CODLOJA AS CD_LOJA,
                                        I.CodProduto as CD_PRODUTO,   
                                        I.VALORNOPDV AS VALORNOPDV,
                                        PRODUTOS.CONFIG AS CONFIG_PROD,
                                        isnull(D.VALORNOPDV, 0) as VALORNOPDV_D,
                                        lojas.SincronizaDrive 
                                       
                                FROM      Inserted I 
                                Left JOIN  Deleted D ON I.CODLOJA = D.CODLOJA AND  I.CodProduto = D.CodProduto
                                INNER JOIN LISTA_PRODUTOS ON I.CodProduto = LISTA_PRODUTOS.CD_PRODUTO  
                                INNER JOIN PRODUTOS ON I.codProduto = PRODUTOS.Codigo 
                                inner join lojas on i.codloja = lojas.Codigo AND LOJAS.CodListaDrive = LISTA_PRODUTOS.CD_LISTA  
                                
                                WHERE  lojas.SincronizaDrive = 1
                                          
                                OPEN NOVO_DRIVE;   
             
                            FETCH NEXT FROM NOVO_DRIVE INTO @COD_LOJA, @CD_PRODUTO,  @VALORNOPDV,@CONFIG, @VALORNOPDV_D, @SincronizaDrive;
                            WHILE @@FETCH_STATUS = 0   
                            BEGIN    
                                SET @TEXTO = convert(nvarchar(10), @VALORNOPDV) + ' ' + convert(nvarchar(10),@VALORNOPDV_D) ;
                                EXECUTE SP_log  @TEXTO, 'DRIVE';
                                IF (SUBSTRING(@CONFIG,1,1) = '1' and @VALORNOPDV <> @VALORNOPDV_D and @SincronizaDrive = 1 )
                                    BEGIN
                                        INSERT INTO [dbo].[DriveFilaExportacoes]
                                            ([CodTipoExportacao]
                                            ,[Detalhe]
                                            ,[DataHoraRegistro]
                                            ,[DataHoraUltimoEnvio]
                                            ,[MensagemErro]
                                            ,[Exportado])
                                        VALUES
                                            (1
                                            ,rtrim(ltrim(convert(nvarchar(20) ,cast (@CD_PRODUTO as bigint))))
                                            ,getdate()
                                            ,null
                                            ,''
                                            ,0)
                                    END
                                    FETCH NEXT FROM NOVO_DRIVE INTO @COD_LOJA, @CD_PRODUTO,  @VALORNOPDV,@CONFIG, @VALORNOPDV_D, @SincronizaDrive;
                            END   
                            CLOSE NOVO_DRIVE;   
                            DEALLOCATE NOVO_DRIVE; 
                                                                
                        END
						GO
			create TRIGGER [dbo].[TG_EXPORTACAO_TELEVO_PEDIDOS]   
                ON [dbo].[Pedidos]
                FOR  UPDATE   
                AS  
                 SET NOCOUNT ON;   
                
                DECLARE @COD_LOJA AS FLOAT ;
                DECLARE @CODIGO AS FLOAT;
                DECLARE @DT_ENTREGA_I AS DATETIME;
                DECLARE @DT_ENTREGA_D AS DATETIME;
                DECLARE @CONFIG_I AS NVARCHAR(50);
                DECLARE @CONFIG_D AS NVARCHAR(50);
                DECLARE @SincronizaDrive AS BIT
         
                set @SincronizaDrive = 0;
                
                DECLARE NOVO_DRIVE_PEDIDO CURSOR 
                FOR SELECT
                            I.CODIGO AS CODIGO,
                            I.DT_ENTREGA as DT_ENTREGA_I,
                            I.CONFIG  AS CONFIG_I,
                            D.DT_ENTREGA as DT_ENTREGA_D,
                            D.CONFIG  AS CONFIG_D,
                            LOJAS.SincronizaDrive 
                                   
                    FROM      Inserted I 
                    Left JOIN  Deleted D ON I.CODIGO = D.CODIGO
                    inner join lojas on i.codloja = lojas.Codigo 
                    WHERE lojas.SincronizaDrive = 1 and i.CodOrigem = 5
                                      
                    OPEN NOVO_DRIVE_PEDIDO;   
         
                FETCH NEXT FROM NOVO_DRIVE_PEDIDO INTO @CODIGO, @DT_ENTREGA_I,  @CONFIG_I ,@DT_ENTREGA_D, @CONFIG_D, @SincronizaDrive;
                WHILE @@FETCH_STATUS = 0   
                BEGIN    
         
                    IF (( @CONFIG_I <> @CONFIG_D ) and @SincronizaDrive = 1 )
                        BEGIN
                            INSERT INTO [dbo].[DriveFilaExportacoes]
                                ([CodTipoExportacao]
                                ,[Detalhe]
                                ,[DataHoraRegistro]
                                ,[DataHoraUltimoEnvio]
                                ,[MensagemErro]
                                ,[Exportado])
                            VALUES
                                (3
                                ,CONVERT(NVARCHAR(10), @CODIGO)
                                ,getdate()
                                ,null
                                ,''
                                ,0)
                        END
                        FETCH NEXT FROM NOVO_DRIVE_PEDIDO INTO @CODIGO, @DT_ENTREGA_I,  @CONFIG_I ,@DT_ENTREGA_D, @CONFIG_D, @SincronizaDrive;
                END   
                CLOSE NOVO_DRIVE_PEDIDO;   
                DEALLOCATE NOVO_DRIVE_PEDIDO; 
                            GO
USE [GESTAO]
GO
/****** Object:  Trigger [dbo].[TG_GERENCIA_TELEVO_EXPORTACAO]    Script Date: 29/04/2020 12:01:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      create TRIGGER [dbo].[TG_GERENCIA_TELEVO_EXPORTACAO]   
                  ON [dbo].[DriveFilaExportacoes]   
                  FOR  INSERT   
                  AS  
                   SET NOCOUNT ON;   
                  
                  DECLARE @TEXTO AS NVARCHAR(100);
                  DECLARE @IS_INSERT AS BIT;      
                  DECLARE @CODEXPORTACAO AS FLOAT;
                  DECLARE @CD_PRODUTO AS FLOAT;
                  DECLARE @DATAHORAREGISTRO AS DATETIME;
                  DECLARE @EXPORTADO AS BIT;
                  DECLARE @ACHOU AS FLOAT; 
                  DECLARE @CODTIPOEXPORTACAO AS FLOAT;
                  DECLARE @TIPO AS INT;
                  DECLARE @TIPO_ATUAL AS INT;         
                  DECLARE @IS_DELETE AS BIT;
           
                  SET @IS_DELETE = 0;
          
                  SET @IS_INSERT= 0;
                  SET @ACHOU = 0;
                  SET @TEXTO = 'ENTROU TG_GERENCIA_LISTA_EXPORTACAO';
                  EXECUTE SP_log  @TEXTO, 'DRIVE';
           
                          DECLARE NOVO_EXPORT CURSOR 
                          FOR SELECT CodExportacao,
                                      CONVERT(FLOAT, DETALHE) AS CD_PRODUTO,
                                      DataHoraRegistro,
                                      EXPORTADO,
                                      CodTipoExportacao                           
           
                              FROM      inserted  I 
                        
                              OPEN NOVO_EXPORT;   
           
                          FETCH NEXT FROM NOVO_EXPORT INTO @CODEXPORTACAO, @CD_PRODUTO, @DATAHORAREGISTRO, @EXPORTADO, @CODTIPOEXPORTACAO;
                          WHILE @@FETCH_STATUS = 0   
                          BEGIN    
                              SET @ACHOU = (SELECT top 1 CODEXPORTACAO FROM DriveFilaExportacoes WHERE ( CodTipoExportacao = 2 or CodTipoExportacao = 1 ) AND EXPORTADO = 0 AND CONVERT(FLOAT, Detalhe) = @CD_PRODUTO AND CODEXPORTACAO <> @CODEXPORTACAO order by CODEXPORTACAO desc);
                             
           
                              IF (@ACHOU > 0 )
                                 
                                  BEGIN         
          
                                     SET @TIPO = (SELECT top 1 CodTipoExportacao FROM DriveFilaExportacoes WHERE ( CodTipoExportacao = 2 or CodTipoExportacao = 1 or CodTipoExportacao = 4 ) AND EXPORTADO = 0 AND CONVERT(FLOAT, Detalhe) = @CD_PRODUTO AND CODEXPORTACAO <> @CODEXPORTACAO order by CODEXPORTACAO desc);
                                             
                                    IF (@CODTIPOEXPORTACAO = 2)
                                        DELETE FROM DriveFilaExportacoes WHERE  ( CodTipoExportacao = 2 ) and CONVERT(FLOAT, Detalhe) = @CD_PRODUTO AND (CodExportacao <> @CODEXPORTACAO);
                                    ELSE
                                        if (@TIPO = 2)
                                            DELETE FROM DriveFilaExportacoes WHERE  ( CodTipoExportacao = 4 or CodTipoExportacao = 2 or CodTipoExportacao = 1 ) and CONVERT(FLOAT, Detalhe) = @CD_PRODUTO AND (CodExportacao = @CODEXPORTACAO OR CodExportacao = @ACHOU );
                                        else
                                            DELETE FROM DriveFilaExportacoes WHERE  ( CodTipoExportacao = 4 or CodTipoExportacao = 2 or CodTipoExportacao = 1 ) and CONVERT(FLOAT, Detalhe) = @CD_PRODUTO AND (CodExportacao <> @CODEXPORTACAO);
                                  END                     
           
                              FETCH NEXT FROM NOVO_EXPORT INTO  @CODEXPORTACAO, @CD_PRODUTO, @DATAHORAREGISTRO, @EXPORTADO, @CODTIPOEXPORTACAO; 
                          END   
                          CLOSE NOVO_EXPORT;   
                          DEALLOCATE NOVO_EXPORT;    
						  
						  
						  
						  
						  
						  
              



