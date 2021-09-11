#!/usr/bin/ruby


lol = nil
print "lol is nil\n" unless lol

lol = 1
if lol
  print "lol is 1\n"
end

arr = { 'lol' => 'bol' }
print arr.[]('lol') << "\n"
