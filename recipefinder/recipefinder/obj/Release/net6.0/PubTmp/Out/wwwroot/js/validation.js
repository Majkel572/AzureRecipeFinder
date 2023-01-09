(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})();


const form = document.getElementById('zdjecie');
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

const toastContainer = document.createElement('div');
toastContainer.classList.add('toast-container');
document.body.appendChild(toastContainer);

function showToast(message, type) {
    // Create the toast element
    const toast = document.createElement('div');
    toast.classList.add('toast');
    toast.innerHTML = message;

    // Add the appropriate class for the toast type (e.g. success, error)
    toast.classList.add(`toast-${type}`);

    // Append the toast to the container
    toastContainer.appendChild(toast);

    // Remove the toast after 3 seconds
    setTimeout(() => {
        toast.remove();
    }, 3000);
}

form.addEventListener('submit', async event => {
    event.preventDefault();

    // Create a new FormData object
    const formData = new FormData();

    // Append the file input element to the FormData object
    formData.append('photo', fileInput.files[0]);
    console.log(fileInput.files[0]);

    // Send the POST request to the API
    fetch('https://localhost:7290/getrecipe', {
        method: 'POST',
        body: formData
    })
    .then(response => {
        if (response.ok) {
          // Parse the response as a blob
          return response.blob();
        } else {
          throw new Error('An error occurred');
        }
      })
      .then(blob => {
        // Create a URL for the file
        const url = URL.createObjectURL(blob);
    
        // Create a link element and set the href to the file URL
        const a = document.createElement('a');
        a.href = url;
        a.download = 'recipe.txt'; // Set the download file name
    
        // Append the link to the body and click it to download the file
        document.body.appendChild(a);
        a.click();
    
        // Remove the link element
        a.remove();
      })
      .catch(error => {
        // Handle any errors
      });
});