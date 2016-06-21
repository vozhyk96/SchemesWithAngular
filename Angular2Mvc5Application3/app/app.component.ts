import {Component} from '@angular/core';
import {CookieService} from 'angular2-cookie/core';

@Component({
    selector: 'my-app',
    templateUrl: '/app/app.component.html',
    providers: [CookieService]
})
export class AppComponent {
    title: string;
    comments: string[];

    constructor(private _cookieService: CookieService) {

        this.title = this._cookieService.get("Title");
        this.comments = [this._cookieService.get("Comment1"), this._cookieService.get("Comment2")];
    }
    addComent(newComment: HTMLInputElement) {
        console.log(newComment.value);
        this.comments.push(newComment.value);
    }


}
