#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

#endregion

namespace RevitAddin_FizzBuzzChallenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //my code starts

            // 5. Filtered Element Collectors
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_TextNotes);
            //collector.WhereElementIsElementType();
            collector.OfClass(typeof(TextNoteType));
           
            //text location
            XYZ mypoint = new XYZ(10,10,0);
            

            Transaction AddText = new Transaction(doc);
            AddText.Start("Create text notes");

            XYZ offset = new XYZ(10,0,0);
            XYZ newPoint = mypoint;

            //number generator
            int total = 0;
            for (int number = 0; number <= 100; number++)
            {
                total = total + number;

                //conditional logic
                string result = "";
                if (number % 3 == 0 && number % 5 != 0)
                {
                    result = "fizz";
                }
                else if (number % 5 == 0 && number % 3 != 0)
                {
                    result = "buzz";
                }
                else if ((number % 3 == 0) && (number % 5 == 0))
                    result = "fizzbuzz";
                else
                {
                    result = number.ToString();
                }
                newPoint = newPoint.Add(offset);

                    //text notes creation
                    TextNote myTextNote = TextNote.Create(doc,
                     doc.ActiveView.Id, newPoint,
                     result + "\n",
                     collector.FirstElementId());
            }  
                






            AddText.Commit();
            

            return Result.Succeeded;
        }
    }
}
