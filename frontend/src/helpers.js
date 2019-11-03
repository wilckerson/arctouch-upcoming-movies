import moment from "moment";

export default {
    getReleaseDateDescription(releaseDate) {
        return moment(releaseDate).format("MMMM D, YYYY");
    },

    getRuntimeDescription(runtime){
        if(!runtime){ return;}
        var timeInMinutes = parseInt(runtime);

        var h = 0;
        var m = 0;
        while(timeInMinutes >= 60){
            timeInMinutes -= 60;
            h++;
        }
        m = timeInMinutes;

        var description = "";
        if(h > 0){
            description += `${h}h `;
        }
        if(m > 0){
            description += `${m}m`;
        }
        return description.trim();
    },

    getGenre(genreList, id) {
        return (genreList || []).find(item => item.id == id) || {};
    },

    getGenreDescription(genreList, genreIds) {
        var genreNameList = (genreIds || []).map(genreId => this.getGenre(genreList, genreId).name);
        var description = genreNameList.join(", ");
        return description;
    },

    getImagePathW200(path) {
        if (!path) {
            return "/img/empty-img.png";
        }

        return `https://image.tmdb.org/t/p/w200${path}`;
    },

    getImagePathW500(path) {
        if (!path) {
            return "/img/empty-img.png";
        }

        return `https://image.tmdb.org/t/p/w500${path}`;
    },

    getImagePathW1280(path) {
        if (!path) {
            return "/img/empty-img.png";
        }

        return `https://image.tmdb.org/t/p/w1280${path}`;
    },
}