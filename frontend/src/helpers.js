import moment from "moment";

export default {
    getReleaseDateDescription(releaseDate) {
        return moment(releaseDate).format("MMMM D, YYYY");
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