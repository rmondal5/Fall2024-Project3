// scripts.js

// Function to show a loading spinner while fetching data
function showLoadingSpinner() {
    document.getElementById('loadingSpinner').style.display = 'block';
}

// Function to hide the loading spinner
function hideLoadingSpinner() {
    document.getElementById('loadingSpinner').style.display = 'none';
}

// Example function to handle the form submission for adding an actor
async function addActor(event) {
    event.preventDefault(); // Prevent the default form submission

    const formData = new FormData(document.getElementById('actorForm'));
    const response = await fetch('/Actors/Create', {
        method: 'POST',
        body: formData,
    });

    if (response.ok) {
        alert('Actor added successfully!');
        window.location.href = '/Actors'; // Redirect to the actors list
    } else {
        alert('Error adding actor. Please try again.');
    }
}

// Example function to fetch actor details
async function fetchActorDetails(actorId) {
    showLoadingSpinner();
    try {
        const response = await fetch(`/Actors/Details/${actorId}`);
        const data = await response.json();
        // Process and display the actor data
        displayActorDetails(data);
    } catch (error) {
        console.error('Error fetching actor details:', error);
    } finally {
        hideLoadingSpinner();
    }
}

// Function to display actor details in the UI
function displayActorDetails(data) {
    document.getElementById('actorName').innerText = data.name;
    document.getElementById('actorBiography').innerText = data.biography;
    // Populate tweets and sentiments if available
    const tweetsContainer = document.getElementById('tweetsContainer');
    tweetsContainer.innerHTML = '';
    data.tweets.forEach(tweet => {
        const tweetElement = document.createElement('div');
        tweetElement.className = 'tweet';
        tweetElement.innerText = `${tweet.Tweet} - Sentiment: ${tweet.Sentiment}`;
        tweetsContainer.appendChild(tweetElement);
    });
}

// Call this function on page load if needed
document.addEventListener('DOMContentLoaded', () => {
    const actorId = document.getElementById('actorId').value;
    if (actorId) {
        fetchActorDetails(actorId);
    }
});
