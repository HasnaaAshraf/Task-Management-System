

//// === Quotes Arrays ===
//const motivationalQuotes = [
//    "Keep going, you're doing great! 💪",
//    "Every small step counts, don't stop! ✨",
//    "Stay focused and complete your tasks! 📝",
//    "You got this! 🚀",
//    "Make today productive! 🔥"
//];

//const completedQuotes = [
//    "Awesome! Task completed! 🎉",
//    "Great job finishing this one! ✅",
//    "Another task done! Keep the momentum! 💡"
//];

//const pendingQuotes = [
//    "Hurry up! Some tasks are still pending! ⏰",
//    "Don't leave your tasks hanging, let's finish them! 🏃‍♀️",
//    "Time to focus and complete your tasks! 💻"
//];

//// === Helper function to get random quote ===
//function getRandomQuote(quotesArray) {
//    return quotesArray[Math.floor(Math.random() * quotesArray.length)];
//}

//// === Show Toast using SweetAlert2 ===
//function showToast(message, icon = 'info') {
//    Swal.fire({
//        toast: true,
//        position: 'top-end',
//        icon: icon,
//        title: message,
//        showConfirmButton: false,
//        timer: 4000,
//        timerProgressBar: true,
//        background: '#f0f8ff', // optional light background
//        iconColor: '#1e90ff',  // blue icon
//    });
//}

//// === Periodic Motivational Quote ===
//setInterval(() => {
//    const quote = getRandomQuote(motivationalQuotes);
//    showToast(quote);
//}, 30000); // كل 30 ثانية

//// === Task Status Quotes ===
//function taskCompletedQuote() {
//    const quote = getRandomQuote(completedQuotes);
//    showToast(quote, 'success');
//}

//function pendingTasksQuote() {
//    const quote = getRandomQuote(pendingQuotes);
//    showToast(quote, 'warning');
//}