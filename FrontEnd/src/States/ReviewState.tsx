export type Review = {
    user: string;
    dateEvent: Date;
    datePlaced: Date;
    stars: string;
    review: string;
}

export const ReviewConstructor = (userName: string ,datePlaced: Date, dateEvent: Date, feedback: string, rating: string) : Review => ({
    user: userName,
    datePlaced: datePlaced,
    dateEvent: dateEvent,
    stars: rating,
    review: feedback
})