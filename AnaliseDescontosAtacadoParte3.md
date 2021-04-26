
## Parte 2 Tarefa 1: 




criar classe de packs virtuais 
colocar uma propriedade para saber se é pack ou modelo de desconto
nas telas listas de acordo com essa propriedade
devemos ter uma relação com o modelo de etiqueta

alterar nomenclaturas

criar desconto para atacado para os associados 

colocar associação da etiqueta na tela de desconto para atacado

Colocar pack virtual ou desconto para atacado no cadastro de produtos 

Arrumar help dos packs mais novos para ajudar o usuário
Verificar a coluna Pack nos grids e o pack nas mensagens
Sigan > frmBusca > coluna grid  (Pack/Atacado)
Importa cupom fiscal > coluna grid
FrmImpressão > Coluna grid (na etiqueta e no cartaz) no cartaz está cortando a descrição
Encarte > 2 mensagens 


  



## --------------------------------------------

### Leve x pague y > Leve 3 pague 2

```
Exemplo de promoção:
Compre 12 unidades do refrigerante XXX e pague apenas 10 unidades.
```


### Pague 1 centavo ou mais no próximo produto > Leve 3 e ganhe 1

```
Exemplo de promoção:
Compre 5 biscoitos da marca XXX e pague 1 centavo ou mais no próximo.
```









### Leve x e Receba desconto por unidade > Leve 3 e ganhe desconto em outro produto diferente R$

```
Exemplo de promoção:
Compre 5 unidades da Pizza XXX e ganhe R$ 0,10 de desconto em cada.
Observação: A 6ª unidade  não entra na promoção, apenas quando fechar 10 unidades.
```


### Leve x e Receba desconto percentual > Leve 3 e ganhe desconto em outro produto diferente %

```
Exemplo de promoção:
Compre 2 unidades da Pizza XXX e ganhe 10% de desconto na compra de um refrigerante da marca HHH.

Exemplo para dar desconto no mesmo produto.
Exemplo de promoção: Compre o produto XXX e receba 10% de desconto
Passos: 
1)Insira 0,1 na quantidade e informe o desconto percentual.
2)Insira o mesmo produto nas duas grades.
```













### Pague menos por unidade > Leve 6 pague menos em cada unid.

```
Exemplo de promoção:
Compre 12 caixas de leite da marca XXX e pague R$ 1,00 cada caixa.
Observação: A 13ª caixa de leite custará o preço original.
```
### Pague x por cento a menos por unidade > Leve 6 pague menos em cada unid. (%)











### Pague menos a partir de x unidades (atacado) > A partir de 6 pague menos R$

```
Exemplo de promoção:
Compre mais de 100 latas de Refri Lata da marca XXX e pague 1 real em cada unidade.
```

### Pague x porcento a menos a partir de x unidades (atacado) > A partir de 6 pague menos %

```
Exemplo de promoção:
Compre mais de 100 unidades de Refri Lata da marca XXX e receba 10% de desconto em cada unidade.
```

















### Pague 1 centavo ou mais em outro produto > Leve 3 e ganhe outro produto diferente R$

```
Exemplo de promoção:
Compre 2 unidades da Pizza XXX e pague 1 centavo ou mais em um bombom da marca YYY.
Observação: O valor de 1 centavo pode ser trocado por outro valor.
```







### Leve RS x e receba desconto por unidade no produto x >  Nas compras acima de RS50,00 ganhe desconto nestes produtos R$

```
Exemplo de promoção:
Compre R$ 50,00 em produtos de fora deste pack e ganhe R$ 2,00 de desconto no produto x.
```
### Leve RS x e receba desconto percentual no produto x > Nas compras acima de RS50,00 ganhe desconto nestes produtos %

```
Exemplo de promoção:
Compre R$ 50,00 em produtos de fora deste pack e ganhe 10 % de desconto no produto x.
```




private Telecon.GestaoComercial.Biblioteca.PackVirtual.ModeloPack _modelos = new Pack.ModeloPack();








           switch ((int)modelo)
            {
                case 01:
                    modeloPack = "Leve X Pague Y";
                    break;

                case 02:
                    modeloPack = "Pague 1 centavo ou mais em outro produto";
                    break;

                case 03:
                    modeloPack = "Pague menos a partir de x unidades (atacado)";
                    break;

                case 04:
                    modeloPack = "Pague menos por unidade";
                    break;

                case 05:
                    modeloPack = "Pague 1 centavo ou mais no próximo produto";
                    break;

                case 06:
                    modeloPack = "Leve x e Receba desconto percentual";
                    break;

                case 07:
                    modeloPack = "Leve x e Receba desconto por unidade";
                    break;

                case 08:
                    modeloPack = "Valor Diferenciado (Preço 2)";
                    break;

                case 09:
                    modeloPack = "Pague x porcento a menos a partir de x unidades (atacado)";
                    break;

                case 10:
                    modeloPack = "Leve R$ X e Receba desconto por unidade no produto x";
                    break;

                case 11:
                    modeloPack = "Leve R$ X e Receba desconto percentual no produto x";
                    break;

                case 12:
                    modeloPack = "Pague x porcento a menos por unidade";
                    break;

                default:
                    modeloPack = "Selecione um modelo";
                    break;
            }