
const clapSound = new Audio("/sounds/cML0Sf33T0M.mp3");


// Quotes Arrays
const motivationalQuotes = [
    "Keep going, you're doing great! 💪",
    "Every small step counts, don't stop! ✨",
    "Stay focused and complete your tasks! 📝",
    "You got this! 💪",
    "Make today productive! 🔥"
];

const completedQuotes = [
    "Awesome! Task completed! 🎉",
    "Great job finishing this one! ✅",
    "Another task done! Keep the momentum! 💡"
];

const pendingQuotes = [
    "Hurry up! Some tasks are still pending! ⏰",
    "Don't leave your tasks hanging, let's finish them! 🏃‍♀️",
    "Time to focus and complete your tasks! 💻"
];

// Helper Functions
function getRandomQuote(quotesArray) {
    return quotesArray[Math.floor(Math.random() * quotesArray.length)];
}

function showToast(message, icon = 'info') {
    Swal.fire({
        toast: true,
        position: 'top-end',
        icon: icon,
        title: message,
        showConfirmButton: false,
        timer: 4000,
        timerProgressBar: true,
        background: '#f0f8ff', 
        iconColor: '#1e90ff',  
    });
}


// Task Quote Functions
function taskCompletedQuote() {
    const quote = getRandomQuote(completedQuotes);
    showToast(quote, 'success');
}

function pendingTasksQuote() {
    const quote = getRandomQuote(pendingQuotes);
    showToast(quote, 'warning');
}


// Deadline Checker
function checkTaskDeadlines() {

    const rows = document.querySelectorAll("tr[data-due]");

    rows.forEach(row => {

        const isCompleted = row.dataset.completed === "true";

        if (isCompleted) return;

        const dueDate = new Date(row.dataset.due);
        const now = new Date();

        const diff = dueDate - now;

        const minutesLeft = diff / (1000 * 60);

        const taskId = row.dataset.taskId;

        // reminder قبل الديدلاين
        if (minutesLeft > 0 && minutesLeft < 5) {

            showToast("⏰ Hurry! This task is almost due!", "warning");

        }

        // لو الوقت انتهى
        if (minutesLeft <= 0 && !row.dataset.alertShown) {

            row.dataset.alertShown = "true";

            Swal.fire({
                title: "⏰ Time is up!",
                text: "This task is overdue. Extend time?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Extend 10 min",
                cancelButtonText: "Ignore"
            }).then((result) => {

                if (result.isConfirmed) {

                    extendTaskTime(taskId);

                }

            });

        }

    });

}

// Extend Task Time
function extendTaskTime(taskId) {

    fetch(`/Task/Extend/${taskId}`, {
        method: "POST"
    })
        .then(response => {

            if (!response.ok) throw new Error();

            showToast("⏳ Time extended successfully!", "success");

            location.reload();

        })
        .catch(() => {

            Swal.fire("Error", "Couldn't extend task time", "error");

        });

}


// Countdown Timer
function updateCountdowns() {

    const rows = document.querySelectorAll("tr[data-due]");

    rows.forEach(row => {

        row.classList.remove("overdue-task");

        const isCompleted = row.dataset.completed === "true";

        const countdownDiv = row.querySelector(".countdown");

        if (!countdownDiv) return;

        if (isCompleted) {

            countdownDiv.innerHTML = "✅ Completed";
            countdownDiv.style.color = "green";
            return;

        }

        const dueDate = new Date(row.dataset.due);
        const now = new Date();

        const diff = dueDate - now;

        if (diff <= 0) {

            countdownDiv.innerHTML = "⏰ Overdue";
            countdownDiv.style.color = "red";
            row.classList.add("overdue-task");
            return;

        }

        const days = Math.floor(diff / (1000 * 60 * 60 * 24));
        const hours = Math.floor((diff / (1000 * 60 * 60)) % 24);
        const minutes = Math.floor((diff / (1000 * 60)) % 60);
        const seconds = Math.floor((diff / 1000) % 60);

        let timeText = "Due in ";

        if (days > 0) timeText += `${days}d `;
        if (hours > 0 || days > 0) timeText += `${hours}h `;
        if (minutes > 0 || hours > 0 || days > 0) timeText += `${minutes}m `;

        timeText += `${seconds}s ⏳`;

        countdownDiv.innerHTML = timeText;


        if (days === 0 && hours === 0 && minutes === 0 && seconds <= 30) {

            countdownDiv.style.color = "red";

        }
        else if (days === 0 && hours === 0 && minutes <= 2) {

            countdownDiv.style.color = "orange";

        }
        else {

            countdownDiv.style.color = "gray";

        }

    });

}


// Page Load
document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll(".task-toggle-checkbox").forEach(checkbox => {

        checkbox.addEventListener("change", function () {

            const taskId = this.dataset.taskId;

            const row = this.closest("tr");
            row.dataset.completed = this.checked;

            fetch(`/Task/ToggleStatus/${taskId}`, {
                method: "POST"
            })
                .then(response => {

                    if (!response.ok) throw new Error("Request failed");

                    if (this.checked) {
                        clapSound.play();
                        taskCompletedQuote();
                    } else {
                        pendingTasksQuote();
                    }

                })
                .catch(error => {
                    console.error(error);
                });

        });

    });

// motivational quotes

setInterval(() => {

    const quote = getRandomQuote(motivationalQuotes);

    showToast(quote);

}, 15000);


// start countdown

updateCountdowns();

setInterval(updateCountdowns, 1000);


// check deadlines

setInterval(checkTaskDeadlines, 10000);

});