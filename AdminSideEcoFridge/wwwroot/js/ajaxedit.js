$('#editUserForm').off('submit').on('submit', function (e) {
    e.preventDefault();

    $.ajax({
        url: $(this).attr('action'),
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            // Log the entire response to see what you get
            console.log("Response: ", response);

            if (response.success) {
                alert(response.message);
                location.reload(); // Refresh the page or update the UI as needed
            } else {
                alert(response.message || "An unknown error occurred."); // Display the server's error message
            }
        },
        error: function (xhr, status, error) {
            // Log more details to the console
            console.error("AJAX Error: ", error);
            console.error("Status: ", status);
            console.error("Response Text: ", xhr.responseText); // Logs the response from the server

            // Display the error details to the user
            alert("An error occurred while updating the user. Please try again.\n\n" +
                "Error Details: " + xhr.responseText);
        }
    });
});
