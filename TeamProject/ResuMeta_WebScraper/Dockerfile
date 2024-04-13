FROM python:3.10-slim-buster

# Set the working directory in the container
WORKDIR /ResuMeta_WebScraper

RUN apt-get update \
    && apt-get install -y \
        g++ \
        gcc \
        make \
        firefox-esr \
    && rm -rf /var/lib/apt/lists/*   

# Install pipenv
RUN pip install pipenv

# Copy Pipfile and Pipfile.lock
COPY Pipfile Pipfile.lock ./

# Install Python dependencies
RUN pipenv install --deploy --system

# Copy the rest of the application code
COPY . .

# ssh
# ENV SSH_PASSWD "root:Docker!"
# RUN apt-get update \
#         && apt-get install -y --no-install-recommends dialog \
#         && apt-get update \
#  && apt-get install -y --no-install-recommends openssh-server \
#  && echo "$SSH_PASSWD" | chpasswd 

# COPY sshd_config /etc/ssh/
# COPY init.sh /usr/local/bin/

# RUN chmod u+x /usr/local/bin/init.sh

# Expose the port your app runs on
EXPOSE 5722 

