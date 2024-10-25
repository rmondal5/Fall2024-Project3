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

// Function to fetch actor details
async function fetchActorDetails(actorId) {
    showLoadingSpinner();
    try {
        const response = await fetch(`/Actors/Details/${actorId}`);
        const data = await response.json();
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

    const tweetsContainer = document.getElementById('tweetsContainer');
    tweetsContainer.innerHTML = '';
    data.tweets.forEach(tweet => {
        const tweetElement = document.createElement('div');
        tweetElement.className = 'tweet';
        tweetElement.innerText = `${tweet.Tweet} - Sentiment: ${tweet.Sentiment}`;
        tweetsContainer.appendChild(tweetElement);
    });
}

// Function to generate AI reviews for movies
async function generateMovieReviews(movieId) {
    showLoadingSpinner();
    try {
        const response = await fetch(`/Movies/GenerateReviews/${movieId}`);
        const data = await response.json();
        displayMovieReviews(data);
    } catch (error) {
        console.error('Error generating movie reviews:', error);
    } finally {
        hideLoadingSpinner();
    }
}

function displayMovieReviews(data) {
    const reviewsContainer = document.getElementById('reviewsContainer');
    reviewsContainer.innerHTML = '';
    data.reviews.forEach(review => {
        const reviewElement = document.createElement('div');
        reviewElement.className = 'review';
        reviewElement.innerHTML = `<p>${review.text}</p><p>Sentiment: ${review.sentiment}</p>`;
        reviewsContainer.appendChild(reviewElement);
    });
    document.getElementById('overallSentiment').innerText = `Overall Sentiment: ${data.overallSentiment}`;
}

// Function to generate tweets for actors
async function generateActorTweets(actorId) {
    showLoadingSpinner();
    try {
        const response = await fetch(`/Actors/GenerateTweets/${actorId}`);
        const data = await response.json();
        displayActorTweets(data);
    } catch (error) {
        console.error('Error generating actor tweets:', error);
    } finally {
        hideLoadingSpinner();
    }
}

function displayActorTweets(data) {
    const tweetsContainer = document.getElementById('tweetsContainer');
    tweetsContainer.innerHTML = '';
    data.tweets.forEach(tweet => {
        const tweetElement = document.createElement('div');
        tweetElement.className = 'tweet';
        tweetElement.innerHTML = `<p>${tweet.text}</p><p>Sentiment: ${tweet.sentiment}</p>`;
        tweetsContainer.appendChild(tweetElement);
    });
    document.getElementById('overallSentiment').innerText = `Overall Sentiment: ${data.overallSentiment}`;
}

// Function to manage movie-actor relationships
async function addActorToMovie(movieId, actorId) {
    showLoadingSpinner();
    try {
        const response = await fetch('/MovieActors/Create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ movieId, actorId }),
        });

        if (response.ok) {
            alert('Actor added to movie successfully!');
            fetchMovieActors(movieId); // Refresh the list of actors for this movie
        } else {
            alert('Error adding actor to movie. Please try again.');
        }

    } catch (error) {
        console.error('Error adding actor to movie:', error);

    } finally {
        hideLoadingSpinner();

    }
}

async function fetchMovieActors(movieId) {
    showLoadingSpinner();
    try {

        const response = await fetch(`/MovieActors/GetActorsForMovie/${movieId}`);
        const data = await response.json();
        displayMovieActors(data);

    } catch (error) {

        console.error('Error fetching movie actors:', error);

    } finally {

        hideLoadingSpinner();

    }
}

function displayMovieActors(actors) {

    const actorsContainer = document.getElementById('movieActorsContainer');
    actorsContainer.innerHTML = '';
    actors.forEach(actor => {

        const actorElement = document.createElement('div');
        actorElement.className = 'actor';
        actorElement.innerText = actor.name;
        actorsContainer.appendChild(actorElement);

    });
}

// Call this function on page load if needed
document.addEventListener('DOMContentLoaded', () => {

    const movieId = document.getElementById('movieId')?.value;
    const actorId = document.getElementById('actorId')?.value;

    if (movieId) {

        fetchMovieActors(movieId);
        generateMovieReviews(movieId);

    }

    if (actorId) {

        fetchActorDetails(actorId);
        generateActorTweets(actorId);

    }

    // Add event listeners for forms if they exist
    const addActorForm = document.getElementById('addActorForm');
    if (addActorForm) {

        addActorForm.addEventListener('submit', addActor);

    }

    const addActorToMovieForm = document.getElementById('addActorToMovieForm');
    if (addActorToMovieForm) {

        addActorToMovieForm.addEventListener('submit', (event) => {

            event.preventDefault();
            const movieId = document.getElementById('movieId').value;
            const actorSelectValue = document.getElementById('actorSelect').value;
            addActorToMovie(movieId, actorSelectValue);

        });
    }
});