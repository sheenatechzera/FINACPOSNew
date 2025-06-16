using System;
using System.Collections.Generic;
using System.Text;

namespace FinacPOS
{
    /// <summary>

    /// Summary description for NumToText.

    /// </summary>


    public class NumToText
    {
        //public string AmountWords(decimal decAmount, string CurrId)
        //{
        //    string AountInWords = ""; // To return the amount in words
        //    CurrencyInfo currencyInfo = new CurrencySP().CurrencyView(CurrId);
        //    decAmount = Math.Round(decAmount, currencyInfo.NoOfDecimalPlace); //Rounding according to decimal places of currency
        //    string strAmount = decAmount.ToString(); // Just keeping the whole amount as string for performing split operation on it
        //    string strAmountinwordsOfIntiger = "";  // To hold amount in words of intiger
        //    string strAmountInWordsOfDecimal = ""; // To hold amoutn in words of decimal part
        //    string[] strPartsArray = strAmount.Split('.'); // Splitting with "." to get intiger part and decimal part seperately
        //    string strDecimaPart = "";                     // To hold decimal part
        //    if (strPartsArray.Length > 1)
        //        if (strPartsArray[1] != null)
        //            strDecimaPart = strPartsArray[1]; // Holding decimal portion if any
        //    if (strPartsArray[0] != null)
        //        strAmount = strPartsArray[0];    // Holding intiger part of amount
        //    else
        //        strAmount = "";
        //    if (strAmount.Trim() != "" && decimal.Parse(strAmount)!=0)
        //        strAmountinwordsOfIntiger = NumberToText(int.Parse(strAmount));
        //    if(strDecimaPart.Trim()!="" && decimal.Parse(strDecimaPart)!=0)
        //        strAmountInWordsOfDecimal=NumberToText(int.Parse(strDecimaPart));
        //    if (SettingsInfo._currencySuffix)
        //    {
        //        // Showing currency as suffix
        //        if (strAmountinwordsOfIntiger != "")
        //            AountInWords = strAmountinwordsOfIntiger + " " + currencyInfo.CurrencyName;
        //        if (strAmountInWordsOfDecimal != "")
        //            AountInWords = AountInWords + " and " + strAmountInWordsOfDecimal + " " + currencyInfo.SubunitName;
        //        AountInWords = AountInWords + " only";
        //    }
        //    else
        //    {
        //        // Showing currency as prefix
        //        if (strAmountinwordsOfIntiger != "")
        //            AountInWords = currencyInfo.CurrencyName + " " + strAmountinwordsOfIntiger;
        //        if (strAmountInWordsOfDecimal != "")
        //            AountInWords = AountInWords + " and " + currencyInfo.SubunitName + " " + strAmountInWordsOfDecimal;
        //        AountInWords = AountInWords + " only";
        //    }
        //    return AountInWords;
        //}
        //public string NumberToText(int number)
        //{
        //    // Converting the number to words
        //    if (number == 0) return "Zero";
        //    if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        //    int[] num = new int[4];
        //    int first = 0;
        //    int u, h, t;
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    if (number < 0)
        //    {
        //        sb.Append("Minus ");
        //        number = -number;
        //    }
        //    string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        //    string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        //    string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        //    string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        //    num[0] = number % 1000; // units 
        //    num[1] = number / 1000;
        //    num[2] = number / 100000;
        //    num[1] = num[1] - 100 * num[2]; // thousands 
        //    num[3] = number / 10000000; // crores 
        //    num[2] = num[2] - 100 * num[3]; // lakhs 
        //    for (int i = 3; i > 0; i--)
        //    {
        //        if (num[i] != 0)
        //        {
        //            first = i;
        //            break;
        //        }
        //    }
        //    for (int i = first; i >= 0; i--)
        //    {
        //        if (num[i] == 0) continue;
        //        u = num[i] % 10; // ones 
        //        t = num[i] / 10;
        //        h = num[i] / 100; // hundreds 
        //        t = t - 10 * h; // tens 
        //        if (h > 0) sb.Append(words0[h] + "Hundred ");
        //        if (u > 0 || t > 0)
        //        {
        //            if (h > 0 || i == 0) sb.Append(" ");
        //            if (t == 0)
        //                sb.Append(words0[u]);
        //            else if (t == 1)
        //                sb.Append(words1[u]);
        //            else
        //                sb.Append(words2[t - 2] + words0[u]);
        //        }
        //        if (i != 0) sb.Append(words3[i - 1]);
        //    }
        //    return sb.ToString().TrimEnd();
        //}

        public string ConvertAmountToWordsForPrint(decimal decAmount, string CurrId)
        {
            string AountInWords = string.Empty; // To return the amount in words
            CurrencyInfo currencyInfo = new CurrencySP().CurrencyView(CurrId);

            decAmount = Math.Round(decAmount, currencyInfo.NoOfDecimalPlace); //Rounding according to decimal places of currency
            string strAmount = decAmount.ToString(); // Just keeping the whole amount as string for performing split operation on it
            string strAmountinwordsOfIntiger = string.Empty;  // To hold amount in words of intiger
            string strAmountInWordsOfDecimal = string.Empty; // To hold amoutn in words of decimal part
            string[] strPartsArray = strAmount.Split('.'); // Splitting with "." to get intiger part and decimal part seperately
            string strDecimaPart = string.Empty;                     // To hold decimal part
            if (strPartsArray.Length > 1)
                if (strPartsArray[1] != null)
                    strDecimaPart = strPartsArray[1]; // Holding decimal portion if any
            if (strPartsArray[0] != null)
                strAmount = strPartsArray[0];    // Holding intiger part of amount
            else
                strAmount = string.Empty; ;
            if (strAmount.Trim() != string.Empty && decimal.Parse(strAmount) != 0)
                strAmountinwordsOfIntiger = ConvertNumberToText(long.Parse(strAmount));
            if (strDecimaPart.Trim() != string.Empty)
            {
                if (Convert.ToDecimal(strDecimaPart) != 0) //Added on 23/Jan/2025 Varis for remove zero halala
                {
                    int decCount = strDecimaPart.Length;
                    strDecimaPart = (Math.Round(decimal.Parse(strDecimaPart), currencyInfo.NoOfDecimalPlace)).ToString();
                    if (strDecimaPart.Length == 1)
                    {
                        if (decCount != 1)
                        {
                            strDecimaPart = "0" + strDecimaPart;
                        }
                        else
                        {
                            strDecimaPart = strDecimaPart + "0";
                        }
                    }
                    strAmountInWordsOfDecimal = ConvertNumberToText(long.Parse(strDecimaPart));
                    //strAmountInWordsOfDecimal = strDecimaPart + "/100";
                }
                

            }

            string[] strPartsWord = strAmountinwordsOfIntiger.Split(' ');
            int count = strPartsWord.Length;
            if (strPartsWord[count - 1] == "And")
            {
                strAmountinwordsOfIntiger = strAmountinwordsOfIntiger.Replace("And", "");
            }

            if (strAmountinwordsOfIntiger != string.Empty)
                AountInWords = currencyInfo.CurrencyName + " " + strAmountinwordsOfIntiger;
            if (strAmountInWordsOfDecimal != string.Empty)
            {
                if (AountInWords != "")
                {
                    //AountInWords = AountInWords + " And " + currencyInfo.SubunitName + " " + strAmountInWordsOfDecimal;
                    AountInWords = AountInWords + " And " + strAmountInWordsOfDecimal + " " + currencyInfo.SubunitName;
                }
                else
                {
                    //AountInWords = currencyInfo.SubunitName + " " + strAmountInWordsOfDecimal;
                    AountInWords = strAmountInWordsOfDecimal + " " + currencyInfo.SubunitName;
                }
                
            }
                
            AountInWords = AountInWords + " Only";
            return AountInWords;
        }


        private string[] numNames = { "", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine", " Ten", " Eleven", " Twelve", " Thirteen", " Fourteen", " Fifteen", " Sixteen", " Seventeen", " Eighteen", " Nineteen" };
        private string[] tensNames = { "", " Ten", " Twenty", " Thirty", " Forty", " Fifty", " Sixty", " Seventy", " Eighty", " Ninety" };
        private string[] specialNames = { "", " Thousand", " Million", " Billion", " Trillion", " Quadrillion", " Quintillion " };

        private String convertLessThanOneThousand(long number)
        {
            String current;

            if (number % 100 < 20)
            {
                current = numNames[number % 100];
                number /= 100;
            }
            else
            {
                current = numNames[number % 10];
                number /= 10;

                current = tensNames[number % 10] + current;
                number /= 10;
            }
            if (number == 0) return current;
           // return numNames[number] + " Hundred And" + current;
            return numNames[number] + " Hundred " + current;
        }

        private String ConvertNumberToText(long number)
        {

            if (number == 0) { return "Zero"; }

            String prefix = "";

            if (number < 0)
            {
                number = -number;
                prefix = "negative";
            }

            String current = "";
            long place = 0;

            do
            {
                long n = number % 1000;
                if (n != 0)
                {
                    String s = convertLessThanOneThousand(n);
                    current = s + specialNames[place] + current;
                }
                place++;
                number /= 1000;
            } while (number > 0);

            return (prefix + current).TrimEnd();
        }
    }
}