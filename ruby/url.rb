#!/usr/bin/ruby

require 'open-uri'

open("http://www.google.com/search?hl=en&q=" << lalal) do |google|
  puts google.read
end
