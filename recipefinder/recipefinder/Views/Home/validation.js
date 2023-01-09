const form = document.getElementById('form');
const fileInput = document.getElementById('file-input');
const preview = document.getElementById('preview');

fileInput.addEventListener('change', event => {
    // Get the selected file
    const file = event.target.files[0];

    // Create a URL for the file
    const url = URL.createObjectURL(file);

    // Set the src attribute of the image element
    preview.src = url;
});

form.addEventListener('submit', async event => {
    event.preventDefault();

    // Create a new FormData object
    const formData = new FormData();

    // Append the file input element to the FormData object
    formData.append('photo', fileInput.files[0]);

    // Send the POST request to the API
    fetch('https://recipefinder7.azurewebsites.net/getrecipe', {
        method: 'POST',
        body: formData
    });
});