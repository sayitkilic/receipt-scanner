# Receipt Scanner
This console application generates the closest output to the actual image of a receipt from the OCR-generated JSON file of the receipt.
## Table of Contents
- [Features](#features)
- [Installation&Usage](#Installation&Usage)
- [How It Works](#howitworks)

## Features
Reads Receipt json file then generates a text output closest to receipt original appearance.

## Installation&Usage
1. Clone the repository: `git clone [https://github.com/sayitkilic/receipt-scanner.git]`
2. Navigate to the project directory: `cd receipt-scanner`
3. Run

## How It Works

* Reads Receipt.json file
* Receipt.json contains the texts in the receipt and their coordinates.
* Deserializes Receipt.json file to a list which includes receipt item model.
* Receipt items in this list are grouped according to whether they are on the same row or not.
* To decide if two receipt items are on the same row;
  * It calculates the midpoint of the height of one receipt item and check if it is within the height of the other receipt item.
  * If this condition holds true for the second receipt item as well, both of them are on the same row.
  * The values on the Y plane were not compared directly because the 'Y' values of the receipt items on the same line are not the same.
* Each group obtained represents one row of the receipt.
* These receipt items in each group are sorted according to their values on the x plane.
* These sorted group items are combined with the ' ' character and a row of the receipt is obtained.
