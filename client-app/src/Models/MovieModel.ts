export interface MovieModel {
    id: number;
    title: string;
    genre: string;
    description: string;
    director: string;
    imageUrl: string;
    videoUrl: string;
    averageRating?: number;
}
