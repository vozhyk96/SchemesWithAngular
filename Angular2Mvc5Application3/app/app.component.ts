import {Component} from '@angular/core';

@Component({
    selector: 'my-app',
    templateUrl: '/app/app.component.html'
})
export class AppComponent {
    title : string;
    comments : string[];
    constructor(){
        this.title = 'Send!';
        this.comments = ['Первый нах!!!1!','Серега пидор азазза'];
    }
    addComent(newComment: HTMLInputElement){
        console.log(newComment.value);
        this.comments.push(newComment.value);
    }
 }
