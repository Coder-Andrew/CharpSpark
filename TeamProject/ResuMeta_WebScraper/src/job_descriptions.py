from bs4 import BeautifulSoup
from selenium import webdriver
from typing import List

def get_job_descriptions(listing_url) -> str:
    firefox_profile = webdriver.FirefoxProfile()
    firefox_profile.set_preference('permissions.default.image', 2)
    
    options = webdriver.FirefoxOptions()
    options.add_argument("--no-sandbox")
    options.add_argument("--headless")
    options.profile = firefox_profile
    driver = webdriver.Firefox(options=options)

    driver.get(listing_url)

    scraper = BeautifulSoup(driver.page_source, "html.parser")
    driver.quit()

    return scraper.find('div', {'id': 'jobDescriptionText'}).get_text(strip=True)

if __name__ == "__main__":
    print("Testing get_job_descriptions.py")
    print(get_job_descriptions("https://www.indeed.com/viewjob?jk=15153394c72df430&q=part+time&l=Independence%2C+OR&tk=1ht5lvgehj5l4800&from=web&advn=7912125640861214&adid=423411435&ad=-6NYlbfkN0DmT0hHWXNtCIwrMjGzfweS-MaUpI60XMmnPqkrTWFVrpHYXbufcTROvCAa2QlSR_o1s6t2OCGWFRtW5Iuxrv2UNgOD6KzPYii0VoogmIerh6e35yWt-z7iZZq7L69M70PnCm-RXW1EgTy_m1oBv8ScBk4tBrGTldkfgGfwG3cMELoY0PJ_pEBOndjHiDpABV7Jtc1s_LPDpWR4OqH6aW6VP98tZY3qUV0OxZV7AdK92_ltbuh06YLGfpaaj92fenisZwGARdvozHMpNQfEFV_vwwgwpTASUlPEMvu-kH4m3v5T80oHED4wEiExuVvDXXWAGGp_Y0Fu7eTe2PM2hGrKWHfTMn8bODfZorHGI9zTsfs_sgK8nrMPrrA4UD-aQ4gtM7nCuAj1RpAyXpnNKafel1EcDXEbHH_WWogCAMx7GeWzns-5tKG93acesNSykX2krpBJvLiFhYpZv9AM_VE3TmVNhNxkjzAdr_SWIRHKeJetBnDvN1hNFsyx4G2qkIlbkD5bNRT1VNWT85YkpXaeS0zVafRqPKQ%3D&pub=4a1b367933fd867b19b072952f68dceb&camk=4HOcmqOLYrACBnNfjfTUTg%3D%3D&xkcb=SoCZ6_M3BljxyqAEv70KbzkdCdPP&xpse=SoAR6_I3BljwkawdqB0JbzkdCdPP&xfps=85d0b46c-5e2a-48a7-bf99-d196b86e2ac4&vjs=3"))
    print("Test complete")

