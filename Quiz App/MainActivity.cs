using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab3
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        TextView quotationTextView;
        string recovered_question = "";

        protected override void OnCreate(Bundle bundle
            )
        {
            
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            if (bundle == null)
            {


                // Create the quote collection and display the current quote
                quoteCollection = new QuoteBank();
                quoteCollection.LoadQuotes();
                quoteCollection.GetNextQuote();




                quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);

                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            }

            //recreating state
            else
            {
                quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
                recovered_question = bundle.GetString("quote");
                quotationTextView.Text = recovered_question;
            }






            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            };

            var checkButton = FindViewById<Button>(Resource.Id.answerButton);
            var answerInput = FindViewById<EditText>(Resource.Id.answerInput);
            var answerOutput = FindViewById<TextView>(Resource.Id.output);
            
            int right_count = 0;
            
            answerOutput.Text = "You have gotten " + right_count + " answers correct";
        checkButton.Click += delegate {
            var result = FindViewById<TextView>(Resource.Id.resultDisplay);

            if (quoteCollection.CurrentQuote.Person == answerInput.Text)
                {
                right_count++;
                answerOutput.Text = "You have gotten " + right_count + " answers correct";
                result.Text = "CORRECT!";
                
            }
            else
            {
                result.Text = "Incorrect, the proper answer is " + quoteCollection.CurrentQuote.Person;
            }
            
                
            };
            var resetButton = FindViewById<Button>(Resource.Id.resetButton);
            resetButton.Click += delegate {
                right_count = 0;
                answerOutput.Text = "You have gotten " + right_count + " answers correct";
                quoteCollection = new QuoteBank();
                quoteCollection.LoadQuotes();
                quoteCollection.GetNextQuote();

                quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("quote", quoteCollection.CurrentQuote.Quotation.ToString());
            

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }


    }
}

