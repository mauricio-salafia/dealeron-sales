# Dealeron - Sales Taxes
## Problem description
There are a variety of items for sale at a store. When a customer purchases items, they receive a receipt. The receipt 
lists all of the items purchased, the sales price of each item (with taxes included), the total sales taxes for all items, 
and the total sales price. 
Basic sales tax applies to all items at a rate of 10% of the item’s list price, with the exception of books, food, and 
medical products, which are exempt from basic sales tax. An import duty (import tax) applies to all imported items at 
a rate of 5% of the shelf price, with no exceptions. 
Write an application that takes input for shopping baskets and returns receipts in the format shown below, calculating 
all taxes and totals correctly. When calculating the sales tax, round the value up to the nearest 5 cents. For example, if 
a taxable item costs $5.60, an exact 10% tax would be $0.56, and the final price after adding the rounded tax of $0.60 
should be $6.20. 

## Assumptions
The input file is encoded with UTF-8
The file structure is Quantity Description at Price
The quantity will always one in the input file, later the products are grouped by the description, price, and tax price.
The performance or the use of big files was not tested
I focus in the structure and architecture of the project

## Testing
Call to the api/billing endpoint to test the assessment.
![image](https://user-images.githubusercontent.com/21999835/208796467-71a2d35b-c28a-45ca-b901-78e79173dba8.png)
Some test files can be found in the example folder
![image](https://user-images.githubusercontent.com/21999835/208796741-a164f048-dbb1-4775-9b02-15d9c47c7755.png)
The response of the endpoint shows the path of the output file
![image](https://user-images.githubusercontent.com/21999835/208796968-31d38aae-e251-4bfb-b868-d0223f221021.png)
