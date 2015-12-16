import React from 'react';

export class Autocomplete extends React.Component {
  constructor(props) {
    super(props);
    this.componentDidMount = this.componentDidMount.bind(this);
    this.render = this.render.bind(this);
  }

  //object for autocomplete is item = {id: '0', Name: 'Some description'}
  componentDidMount() {
    var items = new Bloodhound({
      datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
      queryTokenizer: Bloodhound.tokenizers.whitespace,
      identify : (item) => {return item.VenueId},
      //everything is fetched from service - no local data provided
      local: [],  
      //where to get data for Bloodhound engine
      remote: {
        //URL for AJAX call
        url: this.props.url,
        //milliseconds to wait until service call id triggered,
        rateLimitWait: 300,   
        //prepare AJAX call to server - settings is jQuery promise object
        prepare: this.props.constructQueryCallback,
        //transformation od response object before Bloodhound engine operates on it
        transform: (response) => { 
          return response.map((item) => { 
              return { 
                Id: item.Id,
                VenueId: item.VenueId,
                EventId: item.EventId,
                Name: item.Name,
                City: item.City,
                AddressInfo: item.AddressInfo
              }
            });
        }
      }      
    });
    var self = this;

    $('.typeahead').typeahead({
      hint: true,
      highlight: true,
      minLength: 3
    },
    {
      name: 'Query',
      source: items,
      displayKey: 'Name',
      templates: {
          suggestion: (data) => {
            return '<div><p>' + data.Name + '</p><p class="text-muted">' + data.AddressInfo + '</p></div>';
          },
          empty: '<div class="empty-message text-danger"> No places found...</div>'
        },
    }).on('typeahead:selected', (event, data) => {
        self.props.addCallback(event, data);
        $('.typeahead').typeahead('val', '');
        console.debug('Value selected: ' + data.VenueId + ' - ' + data.Name);  
    }).on('typeahead:asyncrequest', function() {
        $('.typeahead').addClass('loading');
    })
    .on('typeahead:asynccancel typeahead:asyncreceive', function() {
        $('.typeahead').removeClass('loading');
    });
  }

  render() {
    return (
        <input type="text" id="queryInput" name="queryInput" className="typeahead form-control wide-input col-md-6" placeholder="What do you wanna do?" />
      );
  }
}

Autocomplete.propTypes = {
    url: React.PropTypes.string.isRequired,
    constructQueryCallback: React.PropTypes.func,
    addCallback: React.PropTypes.func,
  };

Autocomplete.defaultProps = {
    addCallback(event, data){},
    constructQueryCallback(query, settings) {
      console.warning('This default function should be overridden to best fit the use case.');
      queryObject = {q: query}; 
      settings.type = "POST";
      settings.contentType = "application/json; charset=UTF-8";
      settings.data = JSON.stringify(queryObject);

      return settings;
    }
  };

