package pw.azure.recipefinder;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

public class RecipeDisplayActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.recipe_display);
    }

    @Override
    public void onBackPressed() {
        finish();
    }
}
