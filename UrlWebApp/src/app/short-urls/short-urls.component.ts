import {Subject, takeUntil} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NgForm} from "@angular/forms";

interface ShortUrl {
  urlId: number;
  sourceUrl: string;
  shortUrl: string;
  createdAt: string;
  ownerUser: {
    name: string;
  };
}

@Component({
  selector: 'app-short-urls',
  templateUrl: './short-urls.component.html',
  styleUrls: ['./short-urls.component.css'],
})
export class ShortUrlsComponent implements OnInit, OnDestroy {
  shortUrls: ShortUrl[] = [];
  newUrl = '';
  errorMessage = '';
  userRole: string | null = '';
  userName: string | null = '';
  private destroy$ = new Subject<void>();

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.loadShortUrls();
    this.userRole = localStorage.getItem('userRole');
    this.userName = localStorage.getItem('userName');
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadShortUrls(): void {
    this.http
      .get<ShortUrl[]>('https://localhost:7009/api/short-urls', { withCredentials: true })
      .pipe(takeUntil(this.destroy$))
      .subscribe((shortUrls) => {
        this.shortUrls = shortUrls;
      });
  }

  addNewUrl(addUrlForm: NgForm) {
    if (addUrlForm.valid) {
      if (this.newUrl) {
        this.http
          .post<{ id: number }>('https://localhost:7009/api/short-urls', { url: this.newUrl }, { withCredentials: true })
          .pipe(takeUntil(this.destroy$))
          .subscribe(
            (response) => {
              this.loadShortUrls();
              this.newUrl = '';
              this.errorMessage = '';
            },
            (error) => {
              // Handle error, e.g., set error message
              this.errorMessage = 'URL already exists.';
            }
          );
      }
    } else {
      console.error('Invalid URL format.');
    }
  }

  deleteUrl(id: number): void {
    const endpoint = this.userRole === 'Admin'
      ? `https://localhost:7009/api/short-urls/${id}`
      : `https://localhost:7009/api/me/short-urls/${id}`;

    this.http.delete(endpoint, { withCredentials: true })
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loadShortUrls();
      });
  }

  redirectToOriginalUrl(shortUrl: string) {
    window.location.href = shortUrl;
  }

  redirectToDetails(id: number) {
    this.router.navigate(['/short-url-details', id]);
  }
}
