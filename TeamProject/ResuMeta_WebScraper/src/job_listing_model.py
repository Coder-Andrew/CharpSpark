'''
The job listing class represents our data model - what is saved and transferred between db and the main application.
'''

class job_listing:
    def __init__(
            self,
            listing_link,
            #listing_link_anchor,
            listing_title,
            listing_company,
            listing_location,
            ) -> None:    
        self.listing_link = listing_link
        #self.listing_link_anchor = listing_link_anchor
        self.listing_title = listing_title
        self.listing_company = listing_company
        self.listing_location = listing_location

    def __str__(self) -> str:
        return f"{self.listing_title} - {self.listing_company} - {self.listing_location} - {self.listing_link}"