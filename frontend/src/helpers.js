import moment from "moment";

export default {
    getReleaseDateDescription(releaseDate) {
        return moment(releaseDate).format("MMMM D, YYYY");
    },
    getImagePathW200(path) {
        if (!path) {
            return "/img/empty-poster-200.png";
        }

        return `https://image.tmdb.org/t/p/w200${path}`;
    },

    getImagePathW500(path) {
        if (!path) {
            return "/img/empty-poster-200.png";
        }

        return `https://image.tmdb.org/t/p/w500${path}`;
    },
}